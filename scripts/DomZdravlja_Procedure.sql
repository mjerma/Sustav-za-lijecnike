USE DomZdravlja
GO

/*KREDENCIJALI ################################################*/

CREATE PROC GetKredencijali
	@korisnickoIme nvarchar(100)
AS
SELECT *
FROM Kredencijal as k
WHERE k.KorisnickoIme = @korisnickoIme
GO

/*AMBULANTA ##################################################*/

CREATE PROC GetAmbulantaByLijecnik
	@lijecnikId int
AS
BEGIN
	SELECT *
	FROM Ambulanta
	WHERE LijecnikID = @lijecnikId
END
GO

CREATE PROC GetAmbulantaBySestra
	@sestraId int
AS
BEGIN
	SELECT *
	FROM Ambulanta
	WHERE SestraID = @sestraId
END
GO

/*PACIJENT ##################################################*/

CREATE PROC GetPacijenti
	@lijecnikId int
AS
BEGIN
	SELECT p.*, pb.*, 
	z.IDZaposlenik, 
	z.Ime as 'LijecnikIme', 
	z.Prezime as 'LijecnikPrezime',
	z.TipZaposlenikaID,
	z.DatumZaposlenja,
	z.PrebivalisteID as 'LijecnikPrebivalisteID',
	z.KredencijalID as 'LijecnikKredencijalID',
	k.*
	FROM Pacijent as p
	INNER JOIN Prebivaliste as pb
	ON p.PrebivalisteID = pb.IDPrebivaliste
	INNER JOIN Zaposlenik as z
	ON p.LijecnikID = z.IDZaposlenik
	INNER JOIN Kredencijal as k
	ON p.KredencijalID = k.IDKredencijal
	WHERE p.LijecnikID = @lijecnikId
END
GO

CREATE PROC GetPacijent
	@kredencijalId int
AS
BEGIN
	SELECT p.*, pb.*,
	z.IDZaposlenik, 
	z.Ime as 'LijecnikIme', 
	z.Prezime as 'LijecnikPrezime',
	z.TipZaposlenikaID,
	z.DatumZaposlenja,
	z.PrebivalisteID as 'LijecnikPrebivalisteID',
	z.KredencijalID as 'LijecnikKredencijalID'
	FROM Pacijent as p
	INNER JOIN Prebivaliste as pb
	ON p.PrebivalisteID = pb.IDPrebivaliste
	INNER JOIN Zaposlenik as z
	ON p.LijecnikID = z.IDZaposlenik
	WHERE p.KredencijalID = @kredencijalId
END
GO

CREATE PROC GetPacijentByID
	@pacijentId int
AS
BEGIN
	SELECT p.*, pb.*,
	z.IDZaposlenik, 
	z.Ime as 'LijecnikIme', 
	z.Prezime as 'LijecnikPrezime',
	z.TipZaposlenikaID,
	z.DatumZaposlenja,
	z.PrebivalisteID as 'LijecnikPrebivalisteID',
	z.KredencijalID as 'LijecnikKredencijalID'
	FROM Pacijent as p
	INNER JOIN Prebivaliste as pb
	ON p.PrebivalisteID = pb.IDPrebivaliste
	INNER JOIN Zaposlenik as z
	ON p.LijecnikID = z.IDZaposlenik
	WHERE p.IDPacijent = @pacijentId
END
GO

CREATE PROC AddPacijent
	@ime nvarchar(100),
	@prezime nvarchar(100),
	@datumRodenja datetime,
	@spolId int,
	@ulicaBroj nvarchar(100),
	@grad nvarchar(50),
	@telefon nvarchar(50),
	@email nvarchar(100),
	@mbo nvarchar(10),
	@lijecnikId int,
	@korisnickoIme nvarchar(100),
	@salt nvarchar(max),
	@hash nvarchar(max)
AS
BEGIN
	DECLARE @prebivalisteId int, @kredencijalId int

	INSERT INTO Prebivaliste(UlicaBroj, Grad) VALUES (@ulicaBroj, @grad)
	SET @prebivalisteId = SCOPE_IDENTITY()

	INSERT INTO Kredencijal VALUES (@korisnickoIme, @salt, @hash)
	SET @kredencijalId = SCOPE_IDENTITY()

	INSERT INTO Pacijent([Ime], [Prezime], [DatumRodenja], [MBO], [PrebivalisteID], [SpolID], [Telefon], [Email], [DatumUpisa], [LijecnikID], [KredencijalID], [Aktivan])
	VALUES (@ime, @prezime, @datumRodenja, @mbo, @prebivalisteId, @spolId, @telefon, @email, SYSDATETIME(), @lijecnikId, @kredencijalId, 1)
END
GO

CREATE PROC UpdatePacijent
	@idPacijent int,
	@ime nvarchar(100),
	@prezime nvarchar(100),
	@datumRodenja DateTime,
	@spolId int,
	@mbo nvarchar(10),
	@prebivalisteId int,
	@ulicaBroj nvarchar(100),
	@grad nvarchar(50),
	@telefon nvarchar(50),
	@email nvarchar(50),
	@lijecnikId int,
	@aktivan bit
AS
UPDATE Prebivaliste
SET UlicaBroj = @ulicaBroj, Grad = @grad
WHERE IDPrebivaliste = @prebivalisteId

UPDATE Pacijent
SET Ime = @ime,
	Prezime = @prezime,
	DatumRodenja = @datumRodenja,
	MBO = @mbo,
	SpolID = @spolId,
	PrebivalisteID = @prebivalisteId,
	Telefon = @telefon,
	Email = @email,
	LijecnikID = @lijecnikId,
	Aktivan = @aktivan
WHERE IDPacijent = @idPacijent
GO

CREATE PROC UpdateKredencijal
	@kredencijalId int,
	@hash nvarchar(max),
	@salt nvarchar(max)
AS
BEGIN	
	UPDATE Kredencijal
		set [Hash] = @hash,
		Salt = @salt
	WHERE IDKredencijal = @kredencijalId
END
GO

CREATE PROC DeletePacijent
	@idPacijent int
AS
BEGIN
	DECLARE @kredencijalId int
	DELETE FROM Pregled
	WHERE PacijentID = @idPacijent
	DELETE FROM Uputnica
	WHERE PacijentID = @idPacijent
	DELETE FROM Recept
	WHERE PacijentID = @idPacijent
	DELETE FROM Cijepljenje
	WHERE PacijentID = @idPacijent
	DELETE FROM Termin
	WHERE PacijentID = @idPacijent
	SELECT @kredencijalId = KredencijalID FROM Pacijent
	DELETE FROM Pacijent
	WHERE IDPacijent = @idPacijent
	DELETE FROM Kredencijal
	WHERE IDKredencijal = @kredencijalId
END
GO

CREATE PROC OtpustiPacijenta
	@idPacijent int
AS
UPDATE Pacijent
SET Aktivan = 0
WHERE IDPacijent = @idPacijent
GO

/*ZAPOSLENIK ##################################################*/

CREATE PROC GetZaposlenik
	@kredencijalId int
AS
BEGIN
	SELECT *
	FROM Zaposlenik as z
	INNER JOIN TipZaposlenika as tz
	ON tz.IDTipZaposlenika = z.TipZaposlenikaID
	INNER JOIN Prebivaliste as p
	ON p.IDPrebivaliste = z.PrebivalisteID
	WHERE KredencijalID = @kredencijalId
END
GO

CREATE PROC GetZaposlenikByID
	@zaposlenikId int
AS
BEGIN
	SELECT *
	FROM Zaposlenik as z
	INNER JOIN TipZaposlenika as tz
	ON tz.IDTipZaposlenika = z.TipZaposlenikaID
	INNER JOIN Prebivaliste as p
	ON p.IDPrebivaliste = z.PrebivalisteID
	WHERE IDZaposlenik = @zaposlenikId
END
GO

CREATE PROC GetLijecnici
AS
BEGIN
	SELECT *
	FROM Zaposlenik as z
	INNER JOIN TipZaposlenika as tz
	ON tz.IDTipZaposlenika = z.TipZaposlenikaID
END
GO

/*PREGLEDI ###################################################*/
CREATE PROC GetPregledi
	@pacijentId int
AS
BEGIN
	SELECT *
	FROM Pregled as p
	INNER JOIN Dijagnoza as d
	ON p.DijagnozaID = d.IDDijagnoza
	WHERE PacijentID = @pacijentId
END
GO

CREATE PROC GetPregled
	@pregledId int
AS
BEGIN
	SELECT *
	FROM Pregled as p
	INNER JOIN Dijagnoza as d
	ON p.DijagnozaID = d.IDDijagnoza
	WHERE p.IDPregled = @pregledId
END
GO

CREATE PROC AddPregled
	@pacijentId int,
	@dijagnozaId int,
	@napomena nvarchar(max)
AS
INSERT INTO Pregled( PacijentID, Datum, DijagnozaID, Napomena)
VALUES (@pacijentId, SYSDATETIME(), @dijagnozaId, @napomena)
GO

/*RECEPTI ################################################*/

CREATE PROC GetReceptiNarudzbe
	@lijecnikId int
AS
BEGIN
	SELECT r.*, pac.*,
	d.IDDijagnoza, d.MKBSifra, d.Naziv as 'DijagnozaNaziv', d.NazivLatinski,
	l.IDLijek, l.ProizvodacID, l.Naziv as 'LijekNaziv', l.Sastav,
	p.IDProizvodac, p.Naziv as 'ProizvodacNaziv'
	FROM Recept as r
	INNER JOIN Dijagnoza as d
	ON r.DijagnozaID = d.IDDijagnoza
	INNER JOIN Lijek as l
	ON r.LijekID = l.IDLijek
	INNER JOIN Proizvodac as p
	ON l.ProizvodacID = p.IDProizvodac
	INNER JOIN Pacijent as pac
	ON r.PacijentID = pac.IDPacijent
	WHERE Odobren = 0 and 
	LijecnikID = @lijecnikId
END
GO

CREATE PROC GetRecepti
	@pacijentId int
AS
BEGIN
	SELECT r.*, pac.*,
	d.IDDijagnoza, d.MKBSifra, d.Naziv as 'DijagnozaNaziv', d.NazivLatinski,
	l.IDLijek, l.ProizvodacID, l.Naziv as 'LijekNaziv', l.Sastav,
	p.IDProizvodac, p.Naziv as 'ProizvodacNaziv'
	FROM Recept as r
	INNER JOIN Dijagnoza as d
	ON r.DijagnozaID = d.IDDijagnoza
	INNER JOIN Lijek as l
	ON r.LijekID = l.IDLijek
	INNER JOIN Proizvodac as p
	ON l.ProizvodacID = p.IDProizvodac
	INNER JOIN Pacijent as pac
	ON r.PacijentID = pac.IDPacijent
	WHERE PacijentID = @pacijentId
END
GO

CREATE PROC AddRecept
	@pacijentId int,
	@dijagnozaId int,
	@lijekId int,
	@napomena nvarchar(max),
	@ponavljajuci bit,
	@odobren bit
AS
BEGIN
	INSERT INTO Recept([PacijentID], [DijagnozaID], [LijekID], [Datum], [Napomena], [Ponavljajuci], [Odobren])
	VALUES (@pacijentId, @dijagnozaId, @lijekId, SYSDATETIME(), @napomena, @ponavljajuci, @odobren)
END
GO

CREATE PROC UpdateRecept
	@receptId int,
	@dijagnozaId int,
	@lijekId int,
	@napomena nvarchar(max),
	@ponavljajuci bit
AS
BEGIN
	UPDATE Recept
	SET DijagnozaID = @dijagnozaId,
		LijekID = @lijekId,
		Napomena = @napomena,
		Ponavljajuci = @ponavljajuci
	WHERE IDRecept = @receptId
END
GO

CREATE PROC OdobriRecept
	@receptId int
AS
BEGIN
	UPDATE Recept
	SET Datum = SYSDATETIME(), 
		Odobren = 1
	WHERE IDRecept = @receptId
END
GO

CREATE PROC DeleteRecept
	@idRecept int
AS
BEGIN
	DELETE FROM Recept
	WHERE IDRecept = @idRecept 
END
GO

/*UPUTNICE #########################################################*/

CREATE PROC GetUputnice
	@pacijentId int
AS
BEGIN
	SELECT u.*, tu.*, us.*, 
	d.IDDijagnoza,
	d.MKBSifra,
	d.Naziv as 'DijagnozaNaziv',
	d.NazivLatinski
	FROM Uputnica as u
	INNER JOIN TipUputnice as tu
	ON u.TipUputniceID = tu.IDTipUputnice
	INNER JOIN Ustanova as us
	ON u.UstanovaID = us.IDUstanova
	INNER JOIN Dijagnoza as d
	ON u.DijagnozaID = d.IDDijagnoza
	WHERE PacijentID = @pacijentId 
END
GO

CREATE PROC GetTipUputnica
AS
BEGIN
	SELECT *
	FROM TipUputnice
END
GO

CREATE PROC AddUputnica
	@pacijentId int,
	@tipUputniceId int,
	@dijagnozaId int,
	@ustanovaId int,
	@napomena nvarchar(max)
AS
BEGIN
	INSERT INTO Uputnica([TipUputniceID], [PacijentID], [DijagnozaID], [UstanovaID], [Datum], [Napomena])
	VALUES (@tipUputniceId, @pacijentId, @dijagnozaId, @ustanovaId, SYSDATETIME(), @napomena)
END
GO

/*TERMINI ###################################################################*/

CREATE PROC GetTerminiPacijenta
	@pacijentId int
AS
BEGIN
	SELECT t.*, p.*, pb.*,
	z.IDZaposlenik, 
	z.Ime as 'LijecnikIme', 
	z.Prezime as 'LijecnikPrezime',
	z.TipZaposlenikaID,
	z.DatumZaposlenja,
	z.PrebivalisteID as 'LijecnikPrebivalisteID',
	z.KredencijalID as 'LijecnikKredencijalID'
	FROM Termin as t
	INNER JOIN Pacijent as p
	ON t.PacijentID = p.IDPacijent
	INNER JOIN Prebivaliste AS pb
	ON p.PrebivalisteID = pb.IDPrebivaliste
	INNER JOIN Zaposlenik as z
	ON t.LijecnikID = z.IDZaposlenik
	WHERE PacijentID = @pacijentId
END
GO

CREATE PROC GetTerminiLijecnika
	@lijecnikId int
AS
BEGIN
	SELECT t.*, p.*, pr.*,
	z.IDZaposlenik, 
	z.Ime as 'LijecnikIme', 
	z.Prezime as 'LijecnikPrezime',
	z.TipZaposlenikaID,
	z.DatumZaposlenja,
	z.PrebivalisteID as 'LijecnikPrebivalisteID',
	z.KredencijalID as 'LijecnikKredencijalID'
	FROM Termin as t
	INNER JOIN Zaposlenik as z
	ON t.LijecnikID = z.IDZaposlenik
	INNER JOIN Pacijent as p
	ON t.PacijentID = p.IDPacijent
	INNER JOIN Prebivaliste as pr
	ON p.PrebivalisteID = pr.IDPrebivaliste
	WHERE t.LijecnikID = @lijecnikId
END
GO

CREATE PROC AddTermin
	@pacijentId int,
	@lijecnikId int,
	@vrijeme nvarchar(100),
	@datum datetime,
	@napomena nvarchar(max)
AS
INSERT INTO Termin([PacijentID], [LijecnikID], [Datum], [Vrijeme], [Napomena])
VALUES (@pacijentId, @lijecnikId, @datum, @vrijeme, @napomena)
GO

/*PREDEFINIRANI TERMIN #################################################################*/

CREATE PROC GetPredefiniraniTermini
AS
BEGIN
	SELECT * 
	FROM PredefiniraniTermin
END
GO

/*CJEPLJENJE ###################################################################*/

CREATE PROC GetCjepiva	
AS
BEGIN
	SELECT * 
	FROM Cjepivo
END
GO

CREATE PROC GetCijepljenja
	@pacijentId int
AS
BEGIN
	SELECT c.*, cj.*, 
	p.IDProizvodac, p.Naziv as 'ProizvodacNaziv'
	FROM Cijepljenje AS c
	INNER JOIN Cjepivo AS cj
	ON c.CjepivoID = cj.IDCjepivo
	INNER JOIN Proizvodac as p
	ON cj.ProizvodacID = p.IDProizvodac
	WHERE PacijentID = @pacijentId
END
GO

CREATE PROC AddCijepljenje
	@pacijentId int,
	@cjepivoId int,
	@datum datetime,
	@napomena nvarchar(max)
AS
BEGIN
	INSERT INTO Cijepljenje([PacijentID], [CjepivoID], [Datum], [Napomena])
	VALUES (@pacijentId, @cjepivoId, @datum, @napomena)
END
GO

/*DIJAGNOZE #################################################################*/

CREATE PROC GetDijagnoze
AS
BEGIN
	SELECT * FROM Dijagnoza
END
GO

CREATE PROC GetDijagnoza
	@dijagnozaId int
AS
BEGIN
	SELECT * FROM Dijagnoza
	WHERE IDDijagnoza = @dijagnozaId
END
GO


/*LIJEKOVI #################################################################*/

CREATE PROC GetLijekovi
AS
BEGIN
	SELECT l.*, p.IDProizvodac, p.Naziv as 'ProizvodacNaziv'
	FROM Lijek as l
	INNER JOIN Proizvodac as p
	ON l.ProizvodacID = p.IDProizvodac
END
GO

CREATE PROC GetLijek
	@lijekId int
AS
BEGIN
	SELECT l.*, p.IDProizvodac, p.Naziv as 'ProizvodacNaziv'
	FROM Lijek as l
	INNER JOIN Proizvodac as p
	ON l.IDLijek = @lijekId
END
GO

/*USTANOVE #################################################################*/
CREATE PROC GetUstanove
AS
BEGIN
	SELECT *
	FROM Ustanova
END
GO