# Könyváruház – Programterv (Év végi beadandó)

**Verzió:** 2.0
**Állapot:** Frissített terv (backend alap elkészült)

---

# 0. Cél és összefoglaló

A projekt célja egy **könyv webáruház rendszer megvalósítása**, amely lehetővé teszi:

* könyvek böngészését
* online rendelés leadását
* adminisztratív kezelését külön admin felületen

A rendszer három fő komponensből áll:

* **Web felület** – vásárlók számára
* **Admin felület (WPF)** – raktár és rendeléskezelés
* **Backend API** – üzleti logika és adatkezelés

A backend egy **ASP.NET Core alapú REST API**, amelyet a web és az admin alkalmazás is használ.

---

# 1. Technológiák

Backend:

* ASP.NET Core (MVC + REST API)

Adatkezelés:

* Entity Framework Core (Code First)
* SQL Server
* LocalDB fejlesztési környezetben

Frontend:

* HTML
* CSS
* JavaScript
* Bootstrap

Admin alkalmazás:

* WPF (Windows Presentation Foundation)

API dokumentáció:

* Swagger (OpenAPI)

Verziókezelés:

* Git
* GitHub

---

# 2. Projekt struktúra (Solution)

A projekt több rétegre van bontva a jobb karbantarthatóság és bővíthetőség érdekében.

## KonyvAruhaz.Doman

Tartalmazza az üzleti objektumokat (entitásokat).

Példák:

* Konyv
* Kategoria
* Felhasznalo
* Rendeles
* RendelesTetel

Ebben a rétegben **nincs adatbázis vagy API függőség**.

---

## KonyvAruhaz.Szerzodesek

Adatátviteli objektumokat (DTO) tartalmaz.

Feladata:

* API request / response modellek
* Web és WPF közötti szerződés

---

## KonyvAruhaz.Alkalmazas

Az üzleti logika réteg.

Feladata:

* rendelés feladás
* kosár kezelés
* üzleti szabályok kezelése

---

## KonyvAruhaz.Infrastruktura

Az adatelérésért felelős réteg.

Tartalmazza:

* EF Core DbContext
* migrációk
* repository-k
* adatbázis kapcsolódás

---

## KonyvAruhaz.Web

ASP.NET Core web alkalmazás.

Tartalmazza:

* MVC oldalakat
* REST API vezérlőket
* Swagger dokumentációt

---

## KonyvAruhaz.Asztali

WPF admin alkalmazás.

Feladata:

* könyvek kezelése
* kategóriák kezelése
* rendelés státusz kezelés
* raktárkészlet kezelés

---

# 3. Rendszer architektúra

A rendszer réteges architektúrát használ.

```
Felhasználó (Web)
        ↓
ASP.NET Core Web + REST API
        ↓
Application réteg (üzleti logika)
        ↓
Infrastructure réteg (EF Core)
        ↓
SQL Server adatbázis
```

Mind a **Web**, mind a **WPF admin alkalmazás** ugyanazt az API-t használja.

---

# 4. Szerepkörök és felelősségek

## Backend fejlesztő

Feladata:

* API végpontok készítése
* üzleti logika implementálása
* adatbázis struktúra kialakítása
* DTO-k készítése

Nem feladata:

* frontend megjelenítés
* WPF UI

---

## Web frontend fejlesztő

Feladata:

* MVC oldalak készítése
* felhasználói felület
* Bootstrap alapú reszponzív design

Nem feladata:

* adatbázis kezelés
* üzleti logika módosítása

---

## WPF fejlesztő

Feladata:

* admin felület
* könyv és kategória kezelés
* rendelés státusz kezelés

Nem feladata:

* web frontend

---

# 5. Funkciók (MVP – minimum életképes verzió)

## Web (felhasználó)

* könyvek listázása
* könyv részletek megtekintése
* regisztráció
* bejelentkezés
* kosár kezelés
* rendelés leadása

---

## Admin (WPF)

* könyv CRUD
* kategória CRUD
* készlet kezelés
* rendelések listázása
* rendelés státusz kezelés

Státuszok:

* Uj
* FeldolgozasAlatt
* Feladva
* Teljesitve
* Torolve

---

# 6. Adatmodell

## Kategoria

* Id
* Nev

---

## Konyv

* Id
* Cim
* Ar
* KategoriaId
* Aktiv

---

## Felhasznalo

* Id
* Email
* JelszoHash
* Szerepkor

---

## Rendeles

* Id
* FelhasznaloId
* LetrehozasIdeje
* Allapot

---

## RendelesTetel

* Id
* RendelesId
* KonyvId
* Mennyiseg
* EgysegAr

---

# 7. API végpontok

A rendszer REST alapú API-n keresztül kommunikál.

Az API Swagger segítségével dokumentált.

## Könyvek

GET /api/konyvek
GET /api/konyvek/{id}
POST /api/konyvek
PUT /api/konyvek/{id}
DELETE /api/konyvek/{id}

---

## Kategóriák

GET /api/kategoriak
POST /api/kategoriak

---

## Rendelések

POST /api/rendelesek
GET /api/rendelesek
PUT /api/rendelesek/{id}/allapot

---

# 8. Fejlesztési szabályok

* Senki nem dolgozik közvetlenül a **main** branch-en.
* Minden új funkció **feature branch-ben** készül.
* Commit üzenetek legyenek rövidek és érthetők.
* Merge előtt kötelező build és alap teszt.

---

# 9. Fejlesztési sorrend

1. Alap adatmodell és migrációk
2. Könyv API elkészítése
3. Kategória API
4. Swagger dokumentáció
5. WPF admin alap funkciók
6. Web frontend (könyvlista, részletek)
7. Regisztráció és bejelentkezés
8. Kosár és rendelés funkció
9. Rendelés admin kezelés

---

# 10. Biztonság

* jelszavak hash-elve kerülnek tárolásra
* admin műveletek jogosultsághoz kötöttek
* API bemenetek validálása
