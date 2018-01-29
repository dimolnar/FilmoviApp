# FilmoviApp
ASP.NET Core MVC application

FilmoviApp je aplikacija napravljena korištenjem ASP.NET Core MVC framework-a.
Omogućuje unos i pregled podataka o filmovima.

#### Model

Korištene model klase su : Film, Glumac, Redatelj, Žanr i GlumacFilm.
GlumacFilm klasa povezuje klasu Film i Glumac:
```
  public class GlumacFilm
  {
	public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int GlumacId { get; set; }
        public Glumac Glumac { get; set; }
  }
```

#### View

Na svakom view-u je omogućeno pretraživanje (search), stvaranje novih podataka, uređivanje postojećih i prikaz detalja.

##### Tražilice
Postoje pretrage prema upisanoj ključnoj riječi i/ili prema odabiru u drop down listi.

Prilikom pokretanja aplikacije se otvara Home stranica koja sadrži slike.
Za izmjenjivanje slika je korišten `carousel`.

Za daljnji pregled stranice potrebno se prijaviti ili registrirati kao novi korisnik.
Ograničenje pregleda je postignuto dodavanjem `[Authorize]` na željenim pozicijama u kontrolerima.

#### Controller

Za povezivanje modela i view-a koriste se kontroleri.
Kako bi se omugućio prikaz podataka prema željenoj pretrazi korišteni su LINQ upiti.
Primjer:
```
  var glumciFilmovi = from g in _context.GlumacFilm.Include(g => g.Film).Include(g => g.Glumac)
                      select g;
  if (!string.IsNullOrEmpty(film))
  {
	glumciFilmovi = glumciFilmovi.Where(x => x.Film.Naziv == film);
  }
 ```
