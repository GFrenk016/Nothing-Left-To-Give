# PIXEL FORGE JAM #3 — GAME SCRIPT

## 1. TEMA DELLA JAM

**Tema ufficiale:**
**FREEDOM BUT AT WHAT COST?**

### Mia interpretazione

Il tema rappresenta il prezzo necessario per ottenere la libertà.

Il giocatore deve continuamente sacrificare qualcosa di importante per poter continuare il proprio cammino verso l'indipendenza.

Il sacrificio può riguardare:

* risorse;
* strutture;
* ricchezze;
* strumenti;
* persone;
* elementi fondamentali per la sopravvivenza del villaggio.

Più il giocatore si avvicina alla libertà, maggiore sarà il prezzo richiesto.

La domanda centrale diventa quindi:

> **Quanto sei disposto a perdere pur di essere libero?**

---

# 2. IDEA DEL GIOCO

## Titolo

**Nothing Left to Give**

## Genere

**2D Top-down Gestionale / Resource Management / Survival Management**

## Pitch in una frase

> Gestisci un villaggio fondato da schiavi, costruisci la sua economia e sacrifica progressivamente ciò che hai conquistato per comprare la tua libertà, prima che il prezzo diventi troppo alto.

## Concept

Il giocatore controlla il capo di un piccolo villaggio formato da schiavi.

Gli schiavizzatori permettono temporaneamente al villaggio di esistere, ma alla fine di ogni round pretendono un tributo.

Ogni round rappresenta un periodo di tempo durante il quale il giocatore deve raccogliere risorse, costruire strutture e sviluppare il villaggio.

Alla fine del round arriva una richiesta.

Il giocatore deve sacrificare abbastanza valore per soddisfarla.

Se paga, il villaggio sopravvive e passa al round successivo.

Se non riesce a pagare, perde.

Con il passare del tempo il villaggio cresce, ma anche le richieste diventano sempre più pesanti.

Il giocatore deve quindi scegliere continuamente tra:

**crescere oggi** oppure **conservare qualcosa da sacrificare domani**.

---

# 3. CORE GAMEPLAY

## Meccanica principale

La meccanica centrale consiste nel:

> **Produrre valore durante un tempo limitato e decidere quale parte del proprio villaggio sacrificare per continuare a essere libero.**

Durante ogni round il giocatore:

* assegna lavoratori;
* raccoglie risorse;
* costruisce edifici;
* produce beni;
* migliora il villaggio;
* affronta piccoli eventi;
* prepara il tributo.

Il giocatore deve continuamente decidere se utilizzare una risorsa per migliorare il villaggio oppure conservarla per il sacrificio finale.

---

# 4. GAMEPLAY LOOP

Il ciclo principale sarà:

1. Inizia un nuovo round.
2. Viene mostrata la richiesta futura degli schiavizzatori.
3. Il giocatore raccoglie risorse.
4. Costruisce o migliora strutture.
5. Produce oggetti di valore.
6. Affronta eventuali problemi ambientali.
7. Scade il tempo.
8. Gli schiavizzatori arrivano.
9. Il giocatore sceglie cosa sacrificare.
10. Se raggiunge il valore richiesto passa al round successivo.
11. Il costo della libertà aumenta.

## Loop sintetico

**Raccogli → Produci → Costruisci → Sopravvivi → Sacrifica → Avanza → Ripeti**

---

# 5. RISORSE

Per mantenere lo scope limitato saranno presenti poche risorse.

## Risorse principali

### Legno

Utilizzato per:

* costruzioni;
* riparazioni;
* produzione.

Valore economico basso.

---

### Cibo

Utilizzato per:

* mantenere gli abitanti;
* affrontare alcuni eventi;
* sacrificio.

Valore economico medio.

---

### Oro / Valore

Rappresenta la principale risorsa di scambio.

Può essere ottenuta tramite:

* produzione;
* strutture avanzate;
* conversione di altre risorse.

È la risorsa più efficiente per soddisfare le richieste.

---

## Popolazione

La popolazione rappresenta contemporaneamente:

* forza lavoro;
* capacità produttiva;
* valore del villaggio.

In situazioni estreme anche gli abitanti possono diventare parte del sacrificio.

Questo rende le decisioni finali molto più pesanti.

---

# 6. SISTEMA DI VALORE

Ogni elemento possiede un valore.

Esempio:

| Elemento             |      Valore |
| -------------------- | ----------: |
| 10 Legno             |           5 |
| 10 Cibo              |          10 |
| Oro                  | 1 per unità |
| Casa                 |          25 |
| Struttura produttiva |          40 |
| Abitante             |          50 |

Alla fine del round viene richiesto un valore totale.

Esempio:

**Tributo richiesto: 100**

Il giocatore potrebbe sacrificare:

* 50 Oro;
* 1 Casa;
* 25 punti di Cibo.

Totale:

**100**

Il giocatore decide quindi personalmente cosa perdere.

---

# 7. OBIETTIVO

## Obiettivo principale

Sopravvivere il piu possibile cercando di trasformare un piccolo accampamento di schiavi fuggiti in un villaggio sufficientemente potente da non dover più pagare alcun tributo.

---

## Win Condition

Il giocatore vince quando raggiunge un livello di potenza tale da poter rifiutare definitivamente il tributo.

Condizione indicativa:

* raggiungere un determinato livello di Potere;
* costruire l'edificio finale;
* avere abbastanza popolazione e risorse;
* raggiungere il round finale.

A quel punto compare una scelta:

> **PAY**

oppure

> **REFUSE**

Se il villaggio è abbastanza potente, **REFUSE** permette di sconfiggere definitivamente gli schiavizzatori.

Il villaggio è finalmente libero.

Ma la win condition sara difficile da raggiungere, come se fosse una sorta di easter egg

---

## Lose Condition

Il giocatore perde quando:

* non riesce a raggiungere il valore richiesto;
* non possiede più elementi sacrificabili sufficienti;
* la popolazione raggiunge zero.

Messaggio finale:

> **You wanted freedom.
> You had nothing left to pay for it.**

---

# 8. CONTROLLI

| Input          | Azione                             |
| -------------- | ---------------------------------- |
| Mouse sinistro | Selezione, movimento e interazione |
| Mouse destro   | Annulla selezione                  |
| I              | Inventario / Risorse               |
| Shift          | Velocizza il tempo                 |
| ESC            | Pausa                              |

Il gioco deve poter essere controllato quasi completamente tramite mouse.

---

# 9. PERSONAGGIO

## Chi controlliamo?

**Capo Villaggio**

## Chi è?

È il fondatore e leader di un gruppo di schiavi.

Non possiede un nome definito, in modo che il giocatore possa identificarsi maggiormente nel ruolo.

## Obiettivo

Creare una comunità:

* autosufficiente;
* prospera;
* sicura;
* abbastanza potente da diventare definitivamente libera.

## Motivazione

Il protagonista ha vissuto tutta la propria vita sotto il controllo degli schiavizzatori.

Ora cerca di costruire qualcosa che nessuno possa più portargli via.

Ma per ottenere quella libertà dovrà continuamente distruggere parte di ciò che sta cercando di proteggere.

---

# 10. AMBIENTAZIONE

## Luogo

Una grande foresta isolata.

Il villaggio si trova in una piccola radura nascosta tra gli alberi.

La foresta rappresenta contemporaneamente:

* protezione;
* isolamento;
* risorse;
* pericolo.

## Atmosfera

**Malinconica — Inquietante — Opprimente**

Il mondo deve trasmettere l'impressione che il villaggio stia continuamente sopravvivendo sul limite.

Ogni nuova costruzione rappresenta progresso.

Ogni sacrificio distrugge una parte di quel progresso.

---

# 11. PALETTE — SOLO 3 COLORI

## Colore 1

**HEX:** `#1E1A1A`

Utilizzo:

* sfondo;
* ombre;
* notte;
* silhouette;
* bordi;
* testo secondario;
* elementi distrutti;
* Game Over.

### Significato

**Peso / Oppressione**

---

## Colore 2

**HEX:** `#3D5225`

Utilizzo:

* alberi;
* cespugli;
* terreno;
* strutture;
* risorse comuni;
* abitanti;
* elementi naturali.

### Significato

**Sopravvivenza**

---

## Colore 3

**HEX:** `#D7C27A`

Utilizzo:

* risorse preziose;
* oggetti sacrificabili;
* elementi interattivi;
* UI;
* obiettivi;
* selezioni;
* richieste;
* momento del sacrificio.

### Significato

**Valore / Libertà**

---

# 12. STRUTTURA DEL GIOCO

## Inizio

Il giocatore vede una piccola radura.

Sono presenti:

* 1 Municipio;
* 2 Case;
* 2 strutture produttive;
* pochi abitanti.

Compare un breve messaggio:

> **You escaped.
> But they still know where you are.**

Poco dopo:

> **Tribute arrives in 5 minutes.**

Il primo round comincia.

---

# 13. PARTE CENTRALE

Ogni round introduce richieste più difficili.

Il villaggio cresce:

* più abitanti;
* nuove strutture;
* maggiore produzione;
* più possibilità.

Ma contemporaneamente aumenta anche ciò che può essere perso.

Il giocatore inizia quindi a creare un legame con il proprio villaggio.

Sacrificare una Casa al Round 2 è semplice.

Sacrificare una Casa al Round 15 potrebbe significare distruggere una parte fondamentale della propria economia.

---

# 14. FINALE

Il gioco può terminare in due modi.

## Finale normale

Il giocatore non riesce più a pagare.

Gli schiavizzatori riprendono il controllo del villaggio.

Game Over.

---

## Finale easter egg

Il giocatore raggiunge abbastanza Potere.

Alla richiesta finale compare:

> **FINAL TRIBUTE**

Il giocatore può scegliere:

**PAY**

oppure

**REFUSE**

Se sceglie REFUSE e possiede abbastanza Potere:

gli schiavizzatori vengono sconfitti.

Messaggio:

> **Freedom finally has no price.**

---

# 15. ROUND

La difficoltà aumenta progressivamente.

## Round 1–3

Tutorial naturale.

Richieste semplici:

* legno;
* cibo;
* piccoli valori.

Il giocatore impara:

* raccolta;
* costruzione;
* sacrificio.

---

## Round 4–6

Introduzione di:

* strutture più costose;
* eventi ambientali;
* richieste più alte.

---

## Round 7–10

Gli schiavizzatori iniziano a richiedere condizioni particolari.

Esempio:

> 100 Value
>
> * 20 Food

oppure:

> 80 Value
>
> * 1 Structure

---

## Round 11–14

Più richieste durante lo stesso round.

Esempio:

### Mini richiesta

30 Cibo.

### Seconda richiesta

20 Legno.

### Tributo finale

200 Valore.

---

## Round 15+

Modalità avanzata.

Le richieste diventano molto pesanti.

Il giocatore deve iniziare a sacrificare parti importanti del villaggio.

Contemporaneamente si avvicina alla possibilità di ribellarsi.

---

# 16. SCHIAVIZZATORI

## Tipo

**Antagonista principale**

Non compaiono necessariamente fisicamente durante tutto il gioco.

La loro presenza viene rappresentata tramite:

* richieste;
* messaggi;
* timer;
* simboli;
* brevi apparizioni.

## Comportamento

Pretendono continuamente tributi dal villaggio.

Ogni pagamento permette al villaggio di continuare temporaneamente a vivere in libertà.

## Come vengono sconfitti?

Accumlando abbastanza:

**Potere**

Il Potere rappresenta:

* popolazione;
* economia;
* strutture;
* capacità produttiva.

Quando supera una determinata soglia, il giocatore può rifiutare il tributo.

---

# 17. OSTACOLI AMBIENTALI

Gli eventi ambientali devono essere semplici.

## Pioggia

Effetto:

riduce temporaneamente la produzione.

Contromisura:

costruire un deposito o migliorare alcune strutture.

---

## Animale selvatico

Effetto:

può interrompere il lavoro di alcuni abitanti.

Contromisura:

costruire una struttura difensiva.

---

## Carestia

Effetto:

consumo maggiore di Cibo.

---

## Malattia

Effetto:

alcuni lavoratori diventano temporaneamente inutilizzabili.

---

Non devono esserci più di **3–4 eventi** nella versione della jam.

---

# 18. COSTRUZIONI

Per limitare lo scope:

## Municipio

Edificio principale.

Se viene perso:

Game Over.

---

## Casa

Aumenta la popolazione massima.

---

## Taglialegna

Produce Legno.

---

## Fattoria

Produce Cibo.

---

## Mercato

Permette di convertire risorse in Oro.

---

## Torre / Difesa

Aumenta la difesa e il Potere.

---

# 19. PROGRESSIONE

La progressione avviene tramite:

* aumento della popolazione;
* aumento della produzione;
* nuove costruzioni;
* maggiore valore economico;
* richieste più difficili;
* nuovi eventi;
* aumento del Potere.

Il giocatore deve percepire contemporaneamente due sensazioni:

**“Sto diventando più forte.”**

e

**“Ho sempre più cose da perdere.”**

Questa dualità rappresenta il cuore del gioco.

---

# 20. SISTEMA DI POTERE

Il Potere rappresenta quanto il villaggio è diventato indipendente.

Può aumentare tramite:

* popolazione;
* edifici;
* Oro;
* strutture avanzate.

Esempio:

| Fonte     | Potere |
| --------- | -----: |
| Abitante  |     +2 |
| Casa      |     +5 |
| Mercato   |    +10 |
| Torre     |    +20 |

Soglia indicativa per la vittoria:

**150 Potere**

Il numero verrà bilanciato durante lo sviluppo.

---

# 21. AUDIO

## Musica

Atmosfera:

**Malinconica e minimale**

La musica dovrebbe utilizzare pochi strumenti e mantenere un ritmo relativamente lento.

Durante il momento del sacrificio può diventare più cupa.

---

## Sound Effects necessari

* click UI;
* costruzione;
* raccolta;
* sacrificio;
* avviso timer;
* nuova richiesta;
* edificio distrutto;
* vittoria;
* sconfitta.

---

# 22. UI

## Elementi indispensabili

* [x] Menu iniziale
* [x] Risorse
* [x] Popolazione
* [x] Timer
* [x] Numero Round
* [x] Tributo richiesto
* [x] Potere
* [x] Pausa
* [x] Game Over
* [x] Vittoria

## Elementi NON necessari

* barra della vita;
* minimappa;
* quest log;
* inventario complesso.

---

# 23. SCHERMATA DEL SACRIFICIO

Alla fine del round il tempo si ferma.

Compare una schermata centrale.

## Esempio

**ROUND 7 COMPLETE**

**Tribute demanded: 120**

Il giocatore seleziona ciò che vuole sacrificare:

* 30 Oro
* 20 Cibo
* 1 Casa
* 1 Abitante

Indicatore:

**Sacrifice Value: 115 / 120**

Quando raggiunge il valore:

**SUBMIT SACRIFICE**

Il giocatore conferma.

Gli elementi scelti vengono eliminati definitivamente.

Il round successivo comincia.

---

# 24. FEEDBACK VISIVO DEL SACRIFICIO

Il sacrificio deve essere uno dei momenti più importanti del gioco.

Quando qualcosa viene sacrificato:

1. il tempo si ferma;
2. l'elemento selezionato lampeggia;
3. diventa del Colore 3;
4. scompare;
5. lo schermo torna lentamente alla normalità.

Questo deve far percepire che il giocatore ha perso realmente qualcosa.

---

# 25. SCOPE CHECK

## Posso sviluppare il gameplay principale entro 1–2 giorni?

**SÌ**, se inizialmente utilizziamo soltanto:

* Legno;
* Cibo;
* Oro;
* 3 edifici;
* sistema Round;
* sistema Sacrificio.

---

## Posso rendere il gioco giocabile dall'inizio alla fine entro il giorno 3?

**SÌ**, creando inizialmente:

* 5 round;
* crescita automatica della difficoltà;
* vittoria provvisoria;
* Game Over.

Successivamente possiamo estendere i round.

---

## Posso eliminare metà delle feature senza distruggere il concept?

**SÌ.**

Il cuore rimane:

**Produzione → Tributo → Sacrificio → Round successivo**

---

# 26. MUST HAVE / NICE TO HAVE

## MUST HAVE

Senza queste cose il gioco non funziona:

1. **Sistema di raccolta e produzione risorse**
2. **Sistema Round + Timer**
3. **Sistema di costruzione**
4. **Sistema di sacrificio basato sul valore**
5. **Win Condition + Lose Condition**

---

## NICE TO HAVE

Le aggiungo solo se avanza tempo:

1. **Eventi ambientali casuali**
2. **Più tipologie di edifici**
3. **Possibilità di sacrificare abitanti**
4. **Animazioni avanzate**
5. **Eventi narrativi**
6. **Leaderboard / High Score**
7. **Endless Mode**

---

## DA NON FARE

Feature che rischiano di far esplodere lo scope:

* combattimento RTS complesso;
* IA avanzata dei cittadini;
* pathfinding complesso;
* multiplayer;
* diplomazia;
* albero tecnologico enorme;
* decine di risorse;
* procedural generation complessa;
* mappe multiple;
* sistema politico;
* storia con molti dialoghi.

---

# 27. MVP

La prima versione deve contenere soltanto:

### Risorse

* Legno
* Cibo
* Oro

### Edifici

* Municipio
* Casa
* Taglialegna
* Fattoria

### Sistemi

* produzione;
* costruzione;
* timer;
* round;
* tributo;
* sacrificio;
* Game Over;
* vittoria.

Se questa versione funziona, il gioco esiste.

Tutto il resto viene dopo.

---

# 28. PIANO DEI 7 GIORNI

## DAY 1 — CONCEPT + CORE

Obiettivo:

* creare progetto Unity;
* impostare risoluzione;
* impostare palette;
* creare mappa;
* movimento/interazione;
* creare sistema Risorse;
* creare sistema Round.

### Fine giornata

> **“Posso produrre risorse e il round termina.”**

---

# DAY 2 — PROTOTIPO

Obiettivo:

* costruzioni;
* costi;
* produzione automatica;
* schermata sacrificio;
* tributo;
* Game Over;
* vittoria temporanea.

### Fine giornata

> **“Posso giocare una partita completa.”**

---

# DAY 3 — GAME COMPLETO

Obiettivo:

* bilanciamento primi round;
* sistema Potere;
* edificio finale;
* finale positivo;
* finale negativo;
* progressione.

### Fine giornata

> **“Il gioco è completabile dall'inizio alla fine.”**

---

# DAY 4 — CONTENUTI

Obiettivo:

* altri edifici;
* nuovi round;
* eventi ambientali;
* difficoltà crescente;
* bilanciamento economia.

---

# DAY 5 — VISUAL

Obiettivo:

* sprite;
* animazioni;
* UI;
* feedback visivi;
* schermata sacrificio;
* menu.

Controllare continuamente:

**SOLO 3 COLORI.**

---

# DAY 6 — AUDIO + POLISH

Obiettivo:

* musica;
* SFX;
* feedback;
* tutorial;
* bug fixing;
* bilanciamento finale;
* test completo.

---

# DAY 7 — RELEASE

Nessuna nuova feature importante.

Solo:

* bug fixing;
* test;
* build;
* WebGL;
* pagina itch.io;
* screenshot;
* descrizione;
* crediti;
* submission.

---

# 29. DESCRIZIONE DEL TEMA

Testo provvisorio per la submission:

> **The theme of Pixel Forge Jam #3 is “Freedom, But At What Cost?”.**
>
> In Sacrifice, freedom is something the player must continuously pay for.
>
> The player leads a village founded by escaped slaves and must build an economy capable of surviving increasingly demanding tributes.
>
> Every round forces the player to permanently sacrifice resources, buildings or even parts of the community they are trying to protect.
>
> Progress therefore creates a paradox: the stronger the village becomes, the more painful every sacrifice becomes.
>
> The player's ultimate objective is to become powerful enough to finally refuse the price of freedom.

---

# 30. PITCH FINALE

## Titolo

**SACRIFICE**

## Descrizione

**Sacrifice** è un gestionale di sopravvivenza in cui il giocatore guida un piccolo villaggio fondato da schiavi fuggiti.

Raccogli risorse, costruisci edifici e sviluppa la tua comunità mentre un gruppo di schiavizzatori pretende tributi sempre più pesanti.

Alla fine di ogni round dovrai decidere quale parte del tuo progresso sacrificare per poter continuare.

Risorse, edifici e persino abitanti possono diventare il prezzo della tua libertà.

Costruisci abbastanza potere da rompere definitivamente il ciclo.

Ma ricorda:

> **Freedom always has a price.**

---

# 31. IDENTITÀ DEL GIOCO

## Il giocatore deve provare

All'inizio:

**“Posso farcela.”**

Dopo alcuni round:

**“Posso pagare, ma mi costa.”**

Verso la fine:

**“Non voglio perdere quello che ho costruito.”**

Nel finale:

**“È arrivato il momento di smettere di pagare.”**

---

# 32. PILASTRI DEL DESIGN

Tutte le feature devono sostenere almeno uno di questi tre elementi:

### 1. CRESCITA

Il giocatore deve voler sviluppare il villaggio.

### 2. SACRIFICIO

Il giocatore deve perdere permanentemente qualcosa che considera importante.

### 3. LIBERTÀ

Ogni sacrificio deve avvicinare indirettamente il giocatore al momento in cui potrà finalmente rifiutarsi.

Se una meccanica non supporta almeno uno di questi tre pilastri:

**non serve al gioco.**

---

# 33. REGOLA FINALE

Prima di aggiungere una feature chiediti:

> **Migliora direttamente Produzione, Sacrificio o Progressione?**

Se la risposta è NO:

**non aggiungerla.**

# MANTRA DELLA JAM

**PICCOLO → GIOCABILE → COMPLETO → RIFINITO**
