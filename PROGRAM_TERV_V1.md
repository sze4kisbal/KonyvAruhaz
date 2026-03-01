# Könyváruház – Programterv (Év végi beadandó)

## 0. Cél és összefoglaló
A projekt egy könyv webáruház.
- Böngészés és rendelés weben történik.
- Rendelés csak regisztrált felhasználóként lehetséges.
- Admin raktárkezelés és rendeléskezelés WPF asztali alkalmazásból történik.
- Backend: ASP.NET Core (Web/API) + EF Core + SQL Server (LocalDB fejlesztéshez).

## 1. Technológiák
- Backend: ASP.NET Core (MVC + API)
- Adat: EF Core (Code First), SQL Server / LocalDB
- Web: HTML/CSS/JS + Bootstrap (később opcionálisan Vue)
- Asztali: WPF
- Verziókezelés: Git + GitHub

## 2. Projekt struktúra (Solution)
- KonyvAruhaz.Doman
  - Entitások (adatmodellek): Konyv, Kategoria, stb.
  - Csak üzleti objektumok (nincs EF, nincs API)
- KonyvAruhaz.Szerzodesek
  - DTO-k, kérések/válaszok, közös “szerződések” a Web és WPF között
- KonyvAruhaz.Alkalmazas
  - Üzleti logika / szolgáltatások (pl. rendelés feladás, kosár kezelés)
- KonyvAruhaz.Infrastruktura
  - EF Core DbContext, migrációk, repository-k, adat elérés
- KonyvAruhaz.Web
  - MVC (oldalak) + API vezérlők
- KonyvAruhaz.Asztali
  - WPF kliens (admin/raktárkezelés)

## 3. Szerepkörök és felelősségek (Ki mit csinál?)
### 3.1 Backend fejlesztő (API + üzleti logika)
Felel:
- API végpontok (rendelések, könyvek, felhasználók)
- Üzleti szabályok (ki rendelhet, készlet csökkentés, státuszok)
- DTO-k kialakítása

Nem nyúl:
- WPF UI-hoz (kivéve ha közös megbeszélés)
- Frontend kinézethez

### 3.2 Web frontend fejlesztő (MVC + UI)
Felel:
- MVC oldalak: könyvlista, könyv részletek, kosár, rendelés
- Bootstrap alapú reszponzív megjelenés
- opcionálisan: Vue komponensek (ha kell)

Nem nyúl:
- adatbázis migrációkhoz
- üzleti logika mély részeihez

### 3.3 WPF fejlesztő (Admin felület)
Felel:
- Admin bejelentkezés (ha kell)
- Raktárkezelés (készlet, könyvek felvétele/módosítása)
- Rendelések kezelése (státusz váltás)

Nem nyúl:
- web UI-hoz

## 4. Funkciók (MVP – minimum életképes verzió)
### 4.1 Web (felhasználó)
- Könyvek listázása (szűrés kategória szerint)
- Könyv részletek megtekintése
- Regisztráció, bejelentkezés
- Kosár (hozzáadás/törlés/mennyiség)
- Rendelés leadása (csak belépve)

### 4.2 Admin (WPF)
- Könyv CRUD (felvitel, módosítás, törlés/archiválás)
- Kategória CRUD
- Raktárkészlet kezelés
- Rendelések listázása
- Rendelés státuszok: Uj, FeldolgozasAlatt, Feladva, Teljesitve, Torolve

## 5. Adatmodell (kezdeti)
### 5.1 Kategoria
- Id (int)
- Nev (string)

### 5.2 Konyv
- Id (int)
- Cim (string)
- Ar (decimal 18,2)
- KategoriaId (int)
- Aktiv (bool)

### 5.3 Felhasznalo (később Identity)
- Id
- Email
- JelszoHash / Identity

### 5.4 Rendeles (később)
- Id
- FelhasznaloId
- LetrehozasIdeje
- Allapot
- Osszeg

### 5.5 RendelesTetel (később)
- Id
- RendelesId
- KonyvId
- Mennyiseg
- Egysegar

## 6. API végpontok (terv)
### Könyvek
- GET /api/konyvek
- GET /api/konyvek/{id}
- POST /api/konyvek (admin)
- PUT /api/konyvek/{id} (admin)
- DELETE /api/konyvek/{id} (admin / archiválás)

### Kategóriák
- GET /api/kategoriak
- POST /api/kategoriak (admin)

### Rendelés
- POST /api/rendelesek (felhasználó)
- GET /api/rendelesek (admin)
- PUT /api/rendelesek/{id}/allapot (admin)

## 7. Szabályok (hogy ne legyen káosz)
- Senki nem dolgozik közvetlenül a main branch-en.
- Feature branch neve: feature/<rovid-leiras>
- Commit üzenet: rövid, érthető (pl. "Konyv API GET lista")
- Minden merge előtt: build + alap teszt
- Magyar változónevek oké, de kulcs mezők: Id, KategoriaId, stb.

## 8. Fejlesztési sorrend (javaslat)
1) Alap adatmodell + migrációk (kész)
2) Könyv + kategória CRUD API
3) WPF admin: könyv/kategória kezelés
4) Web MVC: könyvlista + részletek
5) Regisztráció/login
6) Kosár + rendelés
7) Rendelés admin felület WPF-ben




Felhasznalo (Web) 
    ↓
ASP.NET Core Web/API
    ↓
Alkalmazas reteg (uzleti logika)
    ↓
Infrastruktura (EF Core)
    ↓
SQL Server


1. 
Verzio: 1.0
Allapot: Elfogadott tervezet