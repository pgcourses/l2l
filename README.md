# l2l
(I would) like to learn

## Alapvetések
Az alapvetések célja, hogy egyértelmű alapokat biztosítson eldönteni, hogy hol állunk, merre megyünk, mit célzunk és mit nem a munkánkkal.

- Olyan technológiát használunk, amit jelenleg programozóként a profik használnak és még jó darabig fejlődőképes marad (dotnet core).
- Olyan eszközöket használunk, amit a profi programozók is használnak.
- Olyan feladatot választunk, amit nem kell terveznünk, már létezik és körbejárható
- A tervet úgy készítjük, hogy később továbbfejleszthető legyen

## Vázlat
Vázlatunk célja megnevezni az egymásra épülő alkalmazásainkat és megrajzolni az összefüggéseket.

- elsődleges cél: egy webalkalmazás készítése (MVC)
- továbbfejlesztés: az adatok szolgáltatása és az üzleti logika elérése webapin keresztül (MVC)
- továbbfejlesztés: desktop alkalmazás készítése a webapira alapozva (WPF)
- mobil alkalmazás készítése a webapira alapozva (Xamarin)
- továbbfejlesztés: SPA: Single Page Application készítése a webapira alapozva (Blazor)

## Webalkalmazás
Készítsünk egy (továbbfejleszthető) oktató alkalmazást, ahova 
  - oktatók tudnak tanfolyamokat feltölteni, valamint 
  - hallgatók tudnak ilyen tanfolyamokat elvégezni

Ez egy jól áttekinthető, mégis kellően összetett feladat, ahol a full-stack C# fejlesztés minden részébe bepillanthatunk, immár a profi fejlesztő szemével.

## Architektúra

### Első vázlat

Az MVC az adat (Model), a vezérlő (Controller) és a nézet (View) hármasa, induljunk ki ebből és nézzük mi lenne a legegyszerűbb?

```
           DB                              MVC WebApp
      +-----------------+             +--------------------------------------+
      |                 |             |                                      |
      |                 |             |                                      |
      |                 |             | +-------+  +----------+  +---------+ |
      |                 | +-------->  | |       |  |          |  |         | |
      |                 |             | | EF    |  |Controller|  | View    | |
      |                 | <--------+  | | Model |  |          |  |         | |
      |                 |             | |       |  |          |  |         | |
      |                 |             | +-------+  +----------+  +---------+ |
      |                 |             |                                      |
      +-----------------+             +--------------------------------------+

```

Mivel az Entity Framework gyakorlatilag "tükrözi" az adatbázist, lehetne ez az MVC modelje, és már csak a vezérlő és a nézet kell. Igen ám, de mi legyen az API-val ekkor később? Annak minimum az adatokhoz hozzá kell férnie!

### Második vázlat

```
       DB                            EF Model                            MVC WebApp
    +-----------------+           +----------------------+           +--------------------------------------+
    |                 |           |                      |           |                                      |
    |                 | +-------> |                      | +------>  |                                      |
    |                 |           |                      |           | +-------+  +----------+  +---------+ |
    |                 | <-------+ |                      | <------+  | |       |  |          |  |         | |
    |                 |           |                      |           | | View  |  |Controller|  | View    | |
    |                 |           |                      |           | | Model |  |          |  |         | |
    |                 |           |                      |           | |       |  |          |  |         | |
    |                 |           |                      |           | +-------+  +----------+  +---------+ |
    |                 |           |                      |           |                                      |
    +-----------------+           +----------------------+           +--------------------------------------+
```

Ha kiemeljük az EF modelt az alkalmazásunkból, akkor az API ugyan majd később hozzáfér az adatokhoz, de milyen áron? A webalkalmazásban lévő logikai lépéseket meg kell ismételnie, így duplán dolgozunk, vagy duplikálunk, ez ugyan nem rossz megoldás, de még nem is jó.

## Harmadik vázlat

```
                                                                  Service
   DB                            Repository                                              MVC WebApp
+-----------------+           +----------------------+          +-----------+        +--------------------------------------+
|                 |           |                      |          |           |        |                                      |
|                 | +-------> | +------+             | +------> |           | <----+ |                                      |
|                 |           | |      |             |          |           |        | +-------+  +----------+  +---------+ |
|                 | <-------+ | |EF    |             | <------+ |           | +----> | |       |  |          |  |         | |
|                 |           | |      |             |          |           |        | | View  |  |Controller|  | View    | |
|                 |           | |Model |             |          |           |        | | Model |  |          |  |         | |
|                 |           | |      |             |          |           |        | |       |  |          |  |         | |
|                 |           | +------+             |          |           |        | +-------+  +----------+  +---------+ |
|                 |           |                      |          |           |        |                                      |
+-----------------+           +----------------------+          +-----------+        +--------------------------------------+
```

Ha az EF modell mellett kiemeljük a szervezési feladatokat (legyen mondjuk *üzleti logika* a neve, az olyan szakzsargon-pozitív, vagy **Service**), akkor már van olyan pont, ahol az API be tud kapcsolódni, és lehetőség szerint nem duplikálunk majd a megvalósításnál már eleve tervezett módon kilométernyi kódokat.

## Objektumorientált tervezési gondolatok (OOD)
- **Csatolás *(Coupling)*:** ha egy elem függ más elemektől, akkor ezek az elemek csatolásban vannak. 
- **gyenge *(Low)*** ez a csatolás abban az esetben, ha a csatolásban lévő elemek esetén egy változás 
továbbterjedése megállítható.

Első célunk tehát: a **gyenge csatolás (*Low Coupling*)**  elérése a dobozaink között.

- **Kohézió *(Cohesion)*:** Egy elem felelősségeinek egymáshoz való kapcsolata.
- a kohézió **gyenge (*low*)**, ha az adott elemnek túl sok egymástól független felelőssége van.
-  a kohézió **erős (*high*)**, ha az adott elem felelősségei erősen összefüggnek és nagyon koncentráltak.

Célunk tehát az **Erős kohézió *(High Cohesion)*** elérése a dobozokon belül.

- Költségek (vajon megéri?)
  - Függ a rendelkezésre álló erőforrás mértékétől.
  - Függ a rendelkezésre álló időtől is.
  - És leginkább az alkalmazás élettartamától függ.

## Feladatok, felelősségi körök

### DB 
- legyen [ef core támogatása](https://docs.microsoft.com/en-us/ef/core/providers/)
  - adatbázis tervezés és telepítés
- [docker container](https://hub.docker.com) támogatás
- kezdetben sqlight, majd kibővítjük ms sql-re

- Kockázatok
  - teljesítmény
  - relációs adatbázisok

További gondolatok: [műszaki adósság](https://netacademia.blog.hu/2016/06/21/a_muszaki_adossag_fogalma)

### Repository
- CRUD műveletek elvégzése (**C**reate, **R**ead, **U**pdate, **D**elete)
- Listázás (Szűrés, sorbarendezés és lapozás)
- offline adatokat szolgáltat 
  Sosem nem ad vissza IQueryable példányt
- adatmodell-eket szolgáltat
  külön kimeneti modell osztályokat nem fog használni.
- LINQ-t csak itt használunk

- Kockázatok

### Service
- transzformáció az adatmodell és a viewmodel között (Data mapping
- Validálás a ViewModel képességeit meghaladó esetben.
- Minden, amit eddig nem említettünk

- Kockázatok

### Web UI

- http kérés fogadása és válasz küldése
  MVC keretrendszer
- felhasználó azonosítás (authentikation)
  ASP.NET Core Identity
- jogosultságkezelés (authorization)
  ASP.NET Core Identity
- bejövő adatok validálása
  MVC ViewModel

- Kockázatok
  - több alkalmazáson át elosztott bejelentkezés és jogosultságkezelés
    Identity Server

## Továbbiak
- docker containeres fejlesztési környezet
- visual studio code

## Kódstruktúra

Projektek:

- l2l.Data
  - l2l.Data
  - l2l.Repository
- l2l.WebUI
  - l2l.Service
  - l2l.WebUI

  