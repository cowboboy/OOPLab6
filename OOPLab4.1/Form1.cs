namespace OOPLab4._1
{
    public partial class Form1 : Form
    {
        Storage storage; // ��������� � ������������� ������������

        bool isCtrlActive = false;
        bool isCollisionActive = true;
        bool pressedCtrl = false; 

        enum Figures
        {
            Circle,
            Square,
            Triangle,
            Section,
        }
        Figures currentFigure;

        public Form1()
        {
            InitializeComponent();
            storage = new Storage();
            setFigure.DataSource = Enum.GetValues(typeof(Figures));
            setFigure.SelectedItem = Figures.Circle;
            currentFigure = Figures.Circle;
        }

        private void PaintBox_MouseClick(object sender, MouseEventArgs e)
        {
            bool isFirstLayer = true;
            for (int i = storage.size - 1; i >= 0; --i)
            {
                // ������� ����� ������ � �������� checkBoxCtrl
                if (isCtrlActive && pressedCtrl) 
                {
                    if (isCollisionActive)
                    {
                        // ������ ��������� ��� ��������� ����� ������� ���� ��������
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(e.Location) && isFirstLayer)
                        {
                            storage.getObject(i).isActive = true;
                            isFirstLayer = false;
                        }
                        // �� �������� �������� ��������, ��� ��� �������� Ctrl
                    } else
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(e.Location))
                        {
                            storage.getObject(i).isActive = true;
                        }
                    }
                }
                else
                {
                    if (isCollisionActive)
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(e.Location) && isFirstLayer)
                        {
                            storage.getObject(i).isActive = true;
                            isFirstLayer = false;
                        }
                        else
                        {
                            storage.getObject(i).isActive = false;
                        }
                    }
                    else
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(e.Location))
                        {
                            storage.getObject(i).isActive = true;
                        }
                        else
                        {
                            storage.getObject(i).isActive = false;
                        }
                    }
                }
            }

            // ���������� ���������� �� �������
            if (e.Button == MouseButtons.Right) 
            {
                Figure element = null;
                switch (currentFigure)
                {
                    case Figures.Circle:
                        element = new CCircle(e.Location.X, e.Location.Y);
                        break;
                    case Figures.Square:
                        element = new Square(e.Location.X, e.Location.Y);
                        break;
                    case Figures.Triangle:
                        element = new CCircle(e.Location.X, e.Location.Y);
                        break;
                    case Figures.Section:
                        element = new CCircle(e.Location.X, e.Location.Y);
                        break;
                }
                storage.push_back(element);
            }

            // �����������
            PaintBox.Refresh();
        }

        private void PaintBox_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).myPaint(e.Graphics);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                pressedCtrl = true;
            }
            if (e.KeyCode == Keys.Back)
            {
                storage.deleteActiveElements();

                // �����������
                PaintBox.Refresh();
            }
        }

        private void checkBoxCtrl_CheckedChanged(object sender, EventArgs e)
        {
            isCtrlActive = !isCtrlActive;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                pressedCtrl = false;
            }
        }

        private void checkBoxCollision_CheckedChanged(object sender, EventArgs e)
        {
            isCollisionActive = !isCollisionActive;
        }

        private void setFigure_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFigure = (Figures)setFigure.SelectedItem;
        }
    }
}