# Pixel Forge Jam #3 — Sacrifice Roadmap

## Panoramica

| Giorno | Focus | Obiettivo di fine giornata |
|---|---|---|
| Day 1 | Core loop | Produco risorse, finisce il round, pago il tributo |
| Day 2 | Costruzioni + Sacrificio | Posso costruire, sacrificare e perdere |
| Day 3 | Partita completa | Posso iniziare, giocare, vincere o perdere |
| Day 4 | Progressione | Round più complessi, eventi, bilanciamento |
| Day 5 | Art + UI | Il gioco ha una vera identità visiva |
| Day 6 | Audio + Polish | Il gioco è rifinito e comprensibile |
| Day 7 | Release | Build stabile, itch.io, submission |

---

# DAY 1 — CORE LOOP [X]

## Obiettivo

Costruire lo scheletro del gioco.

### Da fare

- creare progetto Unity;
- impostare risoluzione;
- impostare camera;
- impostare palette a 3 colori;
- creare scena principale;
- creare mappa temporanea;
- creare Municipio;
- creare 2–3 lavoratori placeholder;
- creare risorse:
  - Legno
  - Cibo
  - Oro
- creare UI risorse;
- creare sistema tempo;
- creare timer del round;
- creare numero round;
- creare produzione base;
- creare richiesta di fine round.

### Primo prototipo

Esempio:

**Round 1**
- durata: 60 secondi
- richiesta: 20 valore

Durante il round:
- Legno aumenta;
- Cibo aumenta;
- Oro aumenta.

Alla fine:

> TRIBUTE REQUIRED: 20

Il giocatore paga.

Parte il Round 2.

## Fine Day 1

Devi poter dire:

> **“Il ciclo base esiste.”**

### Build ideale

`Produzione → Timer → Tributo → Nuovo Round`

Se questo funziona, Day 1 è riuscito.

---

# DAY 2 — COSTRUZIONI + SACRIFICIO

## Obiettivo

Trasformare la semplice produzione in **scelte gestionali**.

### Costruzioni da implementare

Partiamo solo con:

- Municipio
- Casa
- Taglialegna
- Fattoria

Se avanza tempo:

- Mercato

### Sistema costruzione

Il giocatore deve poter:

- cliccare edificio;
- vedere costo;
- costruire;
- perdere risorse;
- ottenere beneficio.

Esempio:

**Taglialegna**

Costo:  
`20 Legno`

Effetto:  
`+2 Legno/sec`

---

## Sistema Sacrificio

Questa è la feature più importante dell'intera jam.

A fine round:

il tempo si ferma.

Compare:

> TRIBUTE REQUIRED  
> 70 VALUE

Il giocatore sceglie cosa dare.

Esempio:

- 20 Oro
- 10 Cibo
- Casa
- Taglialegna

UI:

**55 / 70**

Poi:

**75 / 70**

Appare:

> SUBMIT SACRIFICE

Confermando, gli elementi vengono eliminati.

## Lose Condition

Se:

**valore disponibile < valore richiesto**

Game Over.

## Fine Day 2

Devi poter dire:

> **“Ora posso realmente sacrificare ciò che ho costruito.”**

A questo punto abbiamo già il cuore di *Sacrifice*.

---

# DAY 3 — PARTITA COMPLETA

Questo è il giorno più importante.

Entro la fine del Day 3 dobbiamo avere tecnicamente **un gioco completo**.

Anche se brutto.

## Obiettivo

Creare:

**Menu → Gameplay → Vittoria/Game Over → Restart**

### Da aggiungere

- menu principale;
- Start Game;
- restart;
- pausa;
- progressione round;
- richieste crescenti;
- sistema Potere;
- condizione di vittoria;
- edificio finale.

---

## Sistema Potere

Esempio:

- Casa = +5
- Taglialegna = +5
- Fattoria = +5
- Mercato = +10
- Abitante = +2

UI:

> POWER: 64

Target:

> FREEDOM: 150 POWER

---

## Monumento della Libertà

Costruzione molto costosa.

Esempio:

- 150 Legno
- 100 Cibo
- 100 Oro

Una volta costruito:

sblocca la possibilità di ribellarsi.

---

## Finale

A un certo punto:

> FINAL TRIBUTE  
> 500 VALUE

Due pulsanti:

**PAY**

**REFUSE**

Se Potere sufficiente:

REFUSE → vittoria.

Altrimenti:

REFUSE → sconfitta.

## Fine Day 3

Devi poter dire:

> **“Posso iniziare una nuova partita e arrivare ai titoli di coda.”**

Da questo momento in poi:

**nessuna feature è indispensabile.**

---

# DAY 4 — PROGRESSIONE + BILANCIAMENTO

Ora rendiamo il gioco interessante.

## Obiettivo

Fare in modo che il Round 10 non sembri semplicemente il Round 1 con numeri più grandi.

### Progressione

#### Round 1–3

Tutorial naturale.

Solo tributo economico.

Esempio:

- 20
- 35
- 50

---

#### Round 4–6

Prime richieste speciali.

Esempio:

> 80 Value  
> + 15 Food

---

#### Round 7–10

Introduzione eventi.

### Eventi possibili

**Heavy Rain**

Produzione:  
`-25%`

per 30 secondi.

---

**Wild Animal**

Una struttura smette temporaneamente di funzionare.

---

**Famine**

Consumo cibo aumentato.

---

Non implementerei più di **3 eventi**.

---

## Round avanzati

Esempio:

### Round 12

Durante il round:

> Give 30 Food

Poi:

> Give 20 Wood

Fine round:

> Tribute: 200

Inizia quella sensazione:

> “Mi stanno dissanguando.”

Ed è esattamente quello che vogliamo.

---

## Bilanciamento

Testare:

- quanto velocemente produco;
- quanto costano gli edifici;
- quanto cresce il tributo;
- quanto facilmente il giocatore può recuperare.

Regola:

La difficoltà deve essere:

**gestibile → tesa → molto difficile**

Non:

**facile → impossibile**

## Fine Day 4

Devi dire:

> **“La partita cambia mentre avanzo.”**

---

# DAY 5 — VISUAL + UX

Ora eliminiamo il prototipo brutto.

## Obiettivo

Dare a *Sacrifice* una vera identità.

### Grafica

Creare:

- Municipio;
- Casa;
- Taglialegna;
- Fattoria;
- Mercato;
- alberi;
- terreno;
- abitanti;
- schiavizzatori;
- risorse/icona.

Tutto esclusivamente con:

`#1E1A1A`

`#3D5225`

`#D7C27A`

---

## UI

### Alto sinistra

Risorse:

- Legno
- Cibo
- Oro

### Alto centro

> ROUND 6

> 03:42

### Alto destra

> POWER 65 / 150

### Parte inferiore

Menu costruzioni.

---

## Tribute UI

Deve essere visivamente importante.

Lo schermo si scurisce.

Centro:

> THE PRICE OF FREEDOM

> 180 VALUE

Poi selezione sacrifici.

Questa schermata deve diventare uno degli elementi più riconoscibili del gioco.

## Fine Day 5

Devi poter dire:

> **“Ora sembra un gioco vero.”**

---

# DAY 6 — AUDIO + POLISH

## Musica

Una sola traccia principale può bastare.

Caratteristiche:

- malinconica;
- lenta;
- minimale;
- ripetitiva senza essere fastidiosa.

Eventualmente:

una seconda variazione per il sacrificio/finale.

---

## Sound Effects

Must have:

- click;
- raccolta;
- costruzione;
- distruzione;
- tributo;
- sacrificio;
- warning timer;
- vittoria;
- Game Over.

---

## Polish

Aggiungere piccoli feedback:

- edificio appare con piccola animazione;
- numeri risorse cambiano;
- oggetto selezionato lampeggia;
- suono quando manca poco alla fine;
- piccolo shake durante sacrificio;
- fade tra round;
- tooltip.

---

## Tutorial

Non creare un tutorial gigantesco.

Usa messaggi brevi.

Esempio:

> Build structures to produce resources.

Poi:

> Prepare for the tribute.

Poi:

> Everything has a value.

Poi:

> Choose what you are willing to lose.

Fine.

## Fine Day 6

Il gioco deve essere:

**comprensibile senza che tu debba spiegare nulla a voce.**

---

# DAY 7 — RELEASE DAY

🚨 **FEATURE FREEZE.**

Non aggiungiamo più roba.

Nemmeno se viene un'idea bellissima.

## Mattina

Test completo.

Fare almeno:

- 3 partite normali;
- 1 partita perdendo subito;
- 1 partita cercando di rompere il gioco;
- 1 partita completa fino alla vittoria.

Controllare:

- timer;
- restart;
- menu;
- sacrifici;
- Game Over;
- victory;
- audio;
- UI;
- WebGL.

---

## Build WebGL

Preparare build definitiva.

Controllare:

- risoluzione browser;
- fullscreen;
- caricamento;
- input;
- audio.

---

## Pagina itch.io

Preparare:

### Titolo

**Sacrifice**

### Tag

- `Management`
- `Strategy`
- `Survival`
- `Pixel Art`
- `Resource Management`
- `Game Jam`

---

## Screenshot

Almeno 3:

1. villaggio;
2. schermata sacrificio;
3. villaggio avanzato/finale.

---

## Descrizione

Inserire:

- pitch;
- controlli;
- tema;
- crediti;
- strumenti utilizzati.

---

## Ultimo controllo palette

Prima della submission controllare ogni singola scena:

- sprite;
- UI;
- testo;
- background;
- menu;
- Game Over;
- Victory;
- particelle.

**Solo 3 colori.**

---

# Milestone principali

### 🔴 Entro Day 1
**Il loop funziona.**

### 🟠 Entro Day 3
**Il gioco è completo.**

### 🟢 Entro Day 6
**Il gioco è pronto per essere pubblicato.**

Il Day 7 deve essere soltanto una rete di sicurezza.

---

# Regola di emergenza

Se entro la sera del **Day 2** qualcosa sta richiedendo troppo tempo, tagliare prima:

- eventi;
- fauna;
- strutture extra;
- animazioni.

Non tagliare mai:

`Round + Produzione + Sacrificio`

Perché quello è il cuore di *Sacrifice*.
