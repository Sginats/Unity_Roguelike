# Unity_bean - Mr. Bean Interactive TV Project

Interaktīvs Unity 2D projekts, kas demonstrē dažādu UI elementu lietojumu ar Mr. Bean tēmatiku. Projekts ietver TV simulāciju ar kanālu pārslēgšanu, skaņas efektiem un dažādiem interaktīviem elementiem.

## Projekta apraksts

Šis projekts ir izveidots kā formatīvais vērtējums, lai demonstrētu prasmi strādāt ar Unity UI elementiem un C# skriptiem. Projekts ietver:

- **TV Simulācija**: Pilnībā funkcionāla televizora imitācija ar iespēju:
  - Ieslēgt/izslēgt TV (Toggle UI elements)
  - Pārslēgties starp vairākiem kanāliem
  - Regulēt skaļumu ar Slider elementu
  
- **Interaktīvie Tēli**: Katrā TV kanālā pieejami vairāki tēli, kuriem:
  - Novietojot peles kursoru, atskaņojas unikāls audio efekts
  - Tēli reaģē uz peles kursoru ar vizuālu feedback
  
- **UI Elementi**: Projektā izmantoti šādi UI elementi:
  - Text (TMP)
  - Input Field
  - Button
  - Slider (skaļuma kontrole)
  - Toggle (TV power, kanālu izvēle)
  - Scroll View
  - Dropdown
  - Image

<img width="1024" height="592" alt="{317F970B-3AE1-4F41-836B-3A868A2178FC}" src="https://github.com/user-attachments/assets/a942fb85-356d-4c46-9892-175447c74b2e" />
<img width="652" height="405" alt="{68115B52-62C3-4961-97CD-3606474ED51C}" src="https://github.com/user-attachments/assets/b2315d1b-bf71-4c58-9c16-8cae0d285c2f" />

## Galvenās funkcionalitātes

### TV Kontrolieris (TVController.cs)
- TV ieslēgšana/izslēgšana
- Kanālu pārslēgšana (2+ kanāli)
- Skaļuma regulēšana

### Tēlu Audio Sistēma (CharacterHoverAudio.cs)
- Audio atskaņošana uz peles kursora novietojumu
- Vizuāls feedback (tēla palielināšana)

### Papildus Funkcionalitāte
- Drag and drop funkcionalitāte
- UI elementu animācija
- Virtuļu ķeršanas spēle

## Tehniskie detaļi

**Unity versija**: 2022.3+
**Platformas**: Windows, WebGL
**Programmēšanas valoda**: C#
**Asset Store resursi**: 2D Environment assets, TextMesh Pro

## Instalācija un palaišana

1. Atvērt projektu ar Unity Editor
2. Atvert "UI_scene" vai "MainMenue" ainu
3. Nospiest Play, lai testētu

## Eksportēšana

Projekts var tikt eksportēts Windows platformai:
1. File → Build Settings
2. Izvēlēties Windows platformu
3. Build
4. Saarhivēt failus un ievietot GitHub Releases

**Darāmo darbu saraksts:**
- [x] UI Button lietojums
- [x] UI Input field lietojums
- [x] UI Text lietojums
- [x] UI Image lietojums
- [x] UI radio button lietojums
- [x] UI slider lietojums
- [x] UI Toggle lietojums
- [x] UI Scroll View lietojums
- [x] UI Dropdown lietojums
- [x] Drag and drop funkcionalitāte
- [x] Audio source lietojums
- [x] Riggid body un collider lietojums
- [x] TV kontroliera izstrāde
- [x] Kanālu pārslēgšanas funkcionalitāte
- [x] Tēlu hover audio sistēma
- [x] UI elementu animācija
- [x] Projekta sagatavošana Windows OS
- [x] Izveidot galvenās izvēlnes ainu
- [x] Izveidot TV ainu
- [x] Integrēt virtuļu ķeršanas spēli
- [x] Git versiju kontrole
