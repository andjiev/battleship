## Опис на играта

Целта на проектот е имплементација на познатата игра **Battleship**. Оваа игра е од натпреварувачки дух и се игра со
максимум двајца играчи или играч против компјутер (како што е и нашиот проект имплементиран) на блок со димензии
10x10 и со вкупно 9 бродови од кои три се со големина еден, два се со големина два, два со големина три, еден со големина четири и еден со големина пет.
Играчот на почеток ги позиционира своите бродови на своите посакувани позиции, додека компјутерот
своите бродови ги позиционира по случаен избор. Двата натпреварувачи разменуваат меѓусебни пукања на блоковите
(секој на спротивниот натпреварувачки блок) каде секое промашување го чини загуба на својот ред, додека секој погодок означува
зачувување на својот ред и можност за победа. Играта завршува кога некој од натпреварувачите ќе ги уништи сите противиничките бродови при што тој се смета за победник.

## Опис на решение

Начинот на реализација на играта е изведен преку архитектурата **MVC**. Користени се класите:
  * **Ship** - Се наоѓа целосна репрезентација на еден брод.

  * **Cell** - Секоја ќелија од одреден брод е сместена во оваа класа.

  * **Grid** - Оваа класа наследува од системската форма **DataGridView** при што е преимплементирана со override методот PaintBackground во кој ја менуваме позадинската слика на блокот.

  * **Еxtensions** - Класа во која се наоѓа имплементација на **DoubleBuffering** кое не е вклучено како Property на **Grid** моделот.

  * **State** - Класа во која преку серијализација ја зачувуваме моменталната состојба при излез од играта за нејзино повторно вчитување.

  * **PlayerController** и **ComputerController** - контролери кои раководат со бродовите.

Формата **Game** ни го претставува **View** делот во која имаме интеракција помеѓу оваа класа и контролерот. Во оваа форма имплементирани се повеќе настани што ја прават оваа игра задоволителна.

## Опис на класата Cell

Оваа класа ја претставува основата од која е составен еден брод. Чуваме позиција за секој дел од бродот, слика која треба да биде прикажана на таа позиција и дали таа позиција е жива (дали противникот ја погодил/уништил).
```csharp
public Point Position { get; set; }
public bool Alive { get; set; }
public Image Img { get; set; }
public bool ChangedOpacity { get; set; }
```
Доколку бродот ја промени позицијата (од хоризонтала во вертикала или обратно), го користиме методот
```csharp
public void SwapImage()
{
    Img.RotateFlip(RotateFlipType.Rotate90FlipX);
}
```
при што ни служи за ротирање на сликата во спротивната насока.  
Како метод кој ни ја намалува моменталната проѕирност на сликата
```csharp
public void Opacity(float opacityValue)
{
    Bitmap bmp = new Bitmap(Img.Width, Img.Height);
    Graphics graphics = Graphics.FromImage(bmp);
    ColorMatrix colormatrix = new ColorMatrix();
    colormatrix.Matrix33 = opacityValue;
    ImageAttributes imgAttribute = new ImageAttributes();
    imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
    graphics.DrawImage(Img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, Img.Width, Img.Height, GraphicsUnit.Pixel, imgAttribute);
    graphics.Dispose();
    Img = bmp;
}
```
е доста корисен при прикажување на брод кој е уништен и при разместување на бродот на невалидна позиција (позиција на која веќе постои брод). Комбинирано, со повик на овој метод, се променува вредноста на **ChangedOpacity**.

## Приказ на играта

Мени
![Menu](https://github.com/andjiev/BattleShip/tree/master/BattleShip/Images/menu.PNG)

Контроли
![Controls](https://github.com/andjiev/BattleShip/tree/master/BattleShip/Images/howTo.PNG)

Состојби на играта
![NewGame](https://github.com/andjiev/BattleShip/tree/master/BattleShip/Images/newGame.PNG)
![InAction](https://github.com/andjiev/BattleShip/tree/master/BattleShip/Images/InAction.PNG)

Бодовна скала
![HighScores](https://github.com/andjiev/BattleShip/tree/master/BattleShip/Images/Highscores.PNG)
