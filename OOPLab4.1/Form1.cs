namespace OOPLab4._1
{
    public partial class Form1 : Form
    {
        Storage storage; // ��������� � ������������� ������������

        bool isCtrlActive = false;
        bool isCollisionActive = true;
        bool pressedCtrl = false;
        bool isMove = false;
        bool isScale = false;

        Point leftTopPaintBox;
        Point rightBottomPaintBox;

        enum Figures
        {
            Circle,
            Square,
            Triangle,
            Section,
        }
        Figures currentFigure;

        Object[] colors = {Color.White, Color.Blue, Color.Green, Color.Yellow};
        Color currentColor;

        Point lastMouseCoords;

        public Form1()
        {
            InitializeComponent();
            storage = new Storage();
            setFigure.DataSource = Enum.GetValues(typeof(Figures));
            setFigure.SelectedItem = Figures.Circle;
            currentFigure = Figures.Circle;
            setColor.Items.AddRange(colors);
            setColor.SelectedItem = Color.White;
            currentColor = Color.White;
            leftTopPaintBox = new Point(0, 0);
            rightBottomPaintBox.X = PaintBox.Width;
            rightBottomPaintBox.Y = PaintBox.Height;
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
                        element = new CCircle(e.Location.X, e.Location.Y, currentColor);
                        break;
                    case Figures.Square:
                        element = new Square(e.Location.X, e.Location.Y, currentColor);
                        break;
                    case Figures.Triangle:
                        element = new CCircle(e.Location.X, e.Location.Y, currentColor);
                        break;
                    case Figures.Section:
                        element = new CCircle(e.Location.X, e.Location.Y, currentColor);
                        break;
                }
                Point leftTop = new Point(), rightBottom = new Point();
                element.getRect(ref leftTop, ref rightBottom);
                if (isNotCollision(leftTop, rightBottom, leftTopPaintBox, rightBottomPaintBox))
                {
                    storage.push_back(element);
                }
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
            if (e.KeyCode == Keys.G)
            {
                isMove = true;
            }
            if (e.KeyCode == Keys.S)
            {
                isScale = true;
            }
            if (e.KeyCode == Keys.Back)
            {
                storage.deleteActiveElements();

                // �����������
                PaintBox.Refresh();
            }
            if (e.KeyCode == Keys.Up && isScale)
            {
                changeScale(1.1f);
            } else if (e.KeyCode == Keys.Down && isScale)
            {
                changeScale(0.9f);
            }
        }

        private void changeScale(float factor)
        {
            Point leftTop = new Point(), rightBottom = new Point();
            getRect(ref leftTop, ref rightBottom);
            Point testRightPoint = new Point((int)(rightBottom.X * factor), (int)(rightBottom.Y * factor));
            if (isNotCollision(leftTop, testRightPoint, leftTopPaintBox, rightBottomPaintBox))
            {
                for (int i = 0; i < storage.size; ++i)
                {
                    if (storage.getObject(i).isActive)
                    {
                        storage.getObject(i).changeScale(factor);
                    }
                }
            }
            PaintBox.Refresh();
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
            if (e.KeyCode == Keys.G)
            {
                isMove = false;
            }
            if (e.KeyCode == Keys.S)
            {
                isScale = false;
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

        private void setColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentColor = (Color)setColor.SelectedItem;
            for (int i = 0; i < storage.size; ++i)
            {
                if (storage.getObject(i).isActive)
                {
                    storage.getObject(i).changeColor((Color)setColor.SelectedItem);
                }
            }
            PaintBox.Refresh();
        }

        private void getRect(ref Point leftTop, ref Point rightBottom)
        {
            storage.getObject(0).getRect(ref leftTop, ref rightBottom);
            Point curLeftTop = new Point();
            Point curRightBottom = new Point();
            for (int i = 1; i < storage.size; ++i)
            {
                Figure curElem = storage.getObject(i);
                curElem.getRect(ref curLeftTop, ref curRightBottom);
                if (curElem.isActive)
                {
                    if (curLeftTop.X < leftTop.X)
                    {
                        leftTop.X = curLeftTop.X;
                    }
                    if (curLeftTop.Y < leftTop.Y)
                    {
                        leftTop.Y = curLeftTop.Y;
                    }
                    if (curRightBottom.X > rightBottom.X)
                    {
                        rightBottom.X = curRightBottom.X;
                    }
                    if (curRightBottom.Y > rightBottom.Y)
                    {
                        rightBottom.Y = curRightBottom.Y;
                    }
                }
            }
        }

        private bool isNotCollision(in Point leftTop, in Point rightBottom, 
                                    in Point leftTopPaintBox, in Point rightBottomPaintBox)
        {
            if (leftTop.X > leftTopPaintBox.X && leftTop.Y > leftTopPaintBox.Y &&
                rightBottom.X < rightBottomPaintBox.X && rightBottom.Y < rightBottomPaintBox.Y)
            {
                return true;
            }
            return false;
        }

        private void PaintBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point leftTop = new Point();
            Point rightBottom = new Point();
            if (isMove)
            {
                getRect(ref leftTop, ref rightBottom);
                int dX = e.Location.X - lastMouseCoords.X;
                int dY = e.Location.Y - lastMouseCoords.Y;
                leftTop.X += dX;
                leftTop.Y += dY;
                rightBottom.X += dX;
                rightBottom.Y += dY;
                if (isNotCollision(leftTop, rightBottom, leftTopPaintBox, rightBottomPaintBox))
                {
                    for (int i = 0; i < storage.size; ++i)
                    {
                        if (storage.getObject(i).isActive)
                        {
                            storage.getObject(i).move(new Point(dX, dY));
                        }
                    }
                }
                PaintBox.Refresh();
            }
            lastMouseCoords = e.Location;
        }
    }
}