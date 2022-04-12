CREATE DATABASE DomZdravlja
GO

USE DomZdravlja
GO

CREATE TABLE TipZaposlenika
(
	IDTipZaposlenika int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Naziv nvarchar(50) NOT NULL
)
GO


CREATE TABLE Proizvodac
(
	IDProizvodac int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Naziv nvarchar(100)
)
GO

CREATE TABLE Ustanova
(
	IDUstanova int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Naziv nvarchar(100) NOT NULL
)
GO

CREATE TABLE Spol
(
	IDSpol int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Naziv nvarchar(100) NOT NULL
)
GO

CREATE TABLE TipUputnice
(
	IDTipUputnice int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Tip nvarchar(100) NOT NULL
)
GO

CREATE TABLE Dijagnoza
(
	IDDijagnoza int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	MKBSifra nvarchar(10) NOT NULL,
	Naziv nvarchar(500) NOT NULL,
	NazivLatinski nvarchar(500) NOT NULL
)
GO

CREATE TABLE Kredencijal
(
	IDKredencijal int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	KorisnickoIme nvarchar(100) NOT NULL,
	Salt nvarchar(max) NOT NULL,
	[Hash] nvarchar(max) NOT NULL
)
GO

CREATE TABLE Prebivaliste
(
	IDPrebivaliste int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	UlicaBroj nvarchar(100) NOT NULL,
	Grad nvarchar(50) NOT NULL
)
GO

CREATE TABLE Lijek
(
	IDLijek int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ProizvodacID int FOREIGN KEY REFERENCES Proizvodac(IDProizvodac) NOT NULL,
	Naziv nvarchar(100) NOT NULL,
	Sastav nvarchar(max) NOT NULL
)
GO

CREATE TABLE Cjepivo
(
	IDCjepivo int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ProizvodacID int FOREIGN KEY REFERENCES Proizvodac(IDProizvodac) NOT NULL,
	Naziv nvarchar(100) NOT NULL,
	Sastav nvarchar(max) NOT NULL
)
GO

CREATE TABLE Zaposlenik
(
	IDZaposlenik int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Ime nvarchar(100) NOT NULL,
	Prezime nvarchar(100) NOT NULL,
	TipZaposlenikaID int FOREIGN KEY REFERENCES TipZaposlenika(IDTipZaposlenika) NOT NULL,
	DatumZaposlenja datetime NOT NULL,
	PrebivalisteID int FOREIGN KEY REFERENCES Prebivaliste(IDPrebivaliste) NOT NULL,
	KredencijalID int FOREIGN KEY REFERENCES Kredencijal(IDKredencijal) NOT NULL
)
GO

CREATE TABLE Ambulanta
(
	IDAmbulanta int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	LijecnikID int FOREIGN KEY REFERENCES Zaposlenik(IDZaposlenik) NOT NULL,
	SestraID int FOREIGN KEY REFERENCES Zaposlenik(IDZaposlenik) NOT NULL
)
GO

CREATE TABLE Pacijent
(
	IDPacijent int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Ime nvarchar(100) NOT NULL,
	Prezime nvarchar(100) NOT NULL,
	DatumRodenja datetime NOT NULL,
	MBO nvarchar(10) NOT NULL,
	PrebivalisteID int FOREIGN KEY REFERENCES Prebivaliste(IDPrebivaliste) NOT NULL,
	SpolID int FOREIGN KEY REFERENCES Spol(IDSpol) NOT NULL,
	Telefon nvarchar(50) NOT NULL,
	Email nvarchar(50) NOT NULL,
	DatumUpisa datetime NOT NULL,
	LijecnikID int FOREIGN KEY REFERENCES Zaposlenik(IDZaposlenik) NOT NULL,
	KredencijalID int FOREIGN KEY REFERENCES Kredencijal(IDKredencijal) NOT NULL,
	Aktivan bit NOT NULL
)
GO

CREATE TABLE Termin
(
	IDTermin int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PacijentID int FOREIGN KEY REFERENCES Pacijent(IDPacijent) NOT NULL,
	LijecnikID int FOREIGN KEY REFERENCES Zaposlenik(IDZaposlenik) NOT NULL,
	Datum datetime NOT NULL,
	Vrijeme nvarchar(100) NOT NULL,
	Napomena nvarchar(max) NULL
)
GO

CREATE TABLE Uputnica
(
	IDUputnica int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	TipUputniceID int FOREIGN KEY REFERENCES TipUputnice(IDTipUputnice) NOT NULL,
	PacijentID int FOREIGN KEY REFERENCES Pacijent(IDPacijent) NOT NULL,
	DijagnozaID int FOREIGN KEY REFERENCES Dijagnoza(IDDijagnoza) NOT NULL,
	UstanovaID int FOREIGN KEY REFERENCES Ustanova(IDUstanova) NOT NULL,
	Datum datetime NOT NULL,
	Napomena nvarchar(max) NULL
)
GO

CREATE TABLE Recept
(
	IDRecept int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PacijentID int FOREIGN KEY REFERENCES Pacijent(IDPacijent) NOT NULL,
	DijagnozaID int FOREIGN KEY REFERENCES Dijagnoza(IDDijagnoza) NOT NULL,
	LijekID int FOREIGN KEY REFERENCES Lijek(IDLijek) NOT NULL,
	Datum datetime NOT NULL,
	Napomena nvarchar(max) NULL,
	Ponavljajuci bit NOT NULL,
	Odobren bit NOT NULL
)
GO

CREATE TABLE Pregled
(
	IDPregled int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PacijentID int FOREIGN KEY REFERENCES Pacijent(IDPacijent) NOT NULL,
	Datum datetime NOT NULL,
	DijagnozaID int FOREIGN KEY REFERENCES Dijagnoza(IDDijagnoza) NOT NULL,
	Napomena nvarchar(max) NULL
)
GO

CREATE TABLE Cijepljenje
(
	IDCijepljenje int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PacijentID int FOREIGN KEY REFERENCES Pacijent(IDPacijent) NOT NULL,
	CjepivoID int FOREIGN KEY REFERENCES Cjepivo(IDCjepivo) NOT NULL,
	Datum datetime NOT NULL,
	Napomena nvarchar(max) NULL
)
GO

CREATE TABLE PredefiniraniTermin
(
	IDPredefiniraniTermin INT PRIMARY KEY IDENTITY NOT NULL,
	PocetakTermina nvarchar(100) NOT NULL
)

GO

