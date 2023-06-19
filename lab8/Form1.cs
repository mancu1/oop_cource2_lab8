/*
Создать приложение, в котором в момент создания формы появляются два 
прямоугольника равных размеров и координат, затем прямоугольники 
начинают разъезжаться по разным углам окна (по диагонали).
после Нарисовать минимальный по размеру прямоугольник, включающий в себя 
оба прямоугольника
 */


using Timer = System.Windows.Forms.Timer;

namespace lab8;

public partial class Form1 : Form
{
    private Rectangle _rect1;
    private Rectangle _rect2;

    private Rectangle _parRect;


    private void CulcParRect()
    {
        _parRect = Rectangle.Union(_rect1, _rect2);
        // add 2px to all demensions
        _parRect.Inflate(2, 2);
    }

    private void StartApp()
    {
        var rectSize = new Size(60, 40);
        int windowWidth = ClientSize.Width;
        int windowHeight = ClientSize.Height;
        
        var center = new Point(windowWidth / 2 - rectSize.Width / 2, windowHeight / 2 - rectSize.Height / 2);

        _rect1 = new Rectangle(center, rectSize);
        _rect2 = new Rectangle(center, rectSize);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics dc = e.Graphics;
        
        // dc.Clear(Color.White);

        _rect1.Offset(1, 1);
        _rect2.Offset(-1, -1);

        CulcParRect();
        dc.DrawRectangle(new Pen(Color.DodgerBlue), _rect1);
        dc.DrawRectangle(new Pen(Color.Firebrick), _rect2);
        dc.DrawRectangle(new Pen(Color.YellowGreen), _parRect);
    }

    private void TimerHandler(object sender, System.EventArgs e)
    {
        Invalidate();
    }

    public Form1()
    {
        
        

        InitializeComponent();
        
        ClientSize = new Size(420, 400);
        this.DoubleBuffered = true;

        //remove windows border and menu
        
        FormBorderStyle = FormBorderStyle.Fixed3D;

        StartApp();

        Timer timer = new Timer();
        timer.Interval = 50;
        timer.Tick += new EventHandler(TimerHandler);
        timer.Start();


    }
}