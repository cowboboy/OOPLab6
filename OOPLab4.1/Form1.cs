namespace OOPLab4._1
{
    public partial class Form1 : Form
    {
        Storage storage; // Хранилище с нарисованными окружностями

        bool isCtrlActive = false;
        bool isCollisionActive = true;
        bool pressedCtrl = false;
        bool isMove = false;
        bool isScale = false;

        MyVector leftTopPaintBox, rightBottomPaintBox;

        enum Figures
        {
            Circle,
            Square,
            Triangle,
        }
        Figures currentFigure;

        Object[] colors = {Color.White, Color.Blue, Color.Green, Color.Yellow};
        Color currentColor;

        MyVector lastMouseCoords;

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
            leftTopPaintBox = new MyVector(0, 0);
            rightBottomPaintBox = new MyVector(PaintBox.Width, PaintBox.Height);
            lastMouseCoords = new MyVector();
            checkBoxCtrl.Checked = true;
        }

        private void PaintBox_MouseClick(object sender, MouseEventArgs e)
        {
            bool isFirstLayer = true;
            for (int i = storage.size - 1; i >= 0; --i)
            {
                // Нажатие мышки вместе с активным checkBoxCtrl
                if (isCtrlActive && pressedCtrl) 
                {
                    if (isCollisionActive)
                    {
                        // Делаем активными все выбранные левой кнопкой мыши элементы
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(new MyVector(e.Location)) && isFirstLayer)
                        {
                            storage.getObject(i).isActive = true;
                            isFirstLayer = false;
                        }
                        // Не затираем активные элементы, так как работает Ctrl
                    } else
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(new MyVector(e.Location)))
                        {
                            storage.getObject(i).isActive = true;
                        }
                    }
                }
                else
                {
                    if (isCollisionActive)
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(new MyVector(e.Location)) && isFirstLayer)
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
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(new MyVector(e.Location)))
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

            // Добавление окружности на полотно
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
                        element = new Triangle(e.Location.X, e.Location.Y, currentColor);
                        break;
                }
                MyVector leftTop = new MyVector();
                MyVector rightBottom = new MyVector();
                element.getRect(leftTop, rightBottom);
                if (isNotCollision(leftTop, rightBottom))
                {
                    storage.push_back(element);
                }
            }

            // Перерисовка
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

                // Перерисовка
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

        /// <summary>
        /// Масштабирует фигуру или группу фигур по заданному коффициенту
        /// </summary>
        /// <param name="factor">Для увеличения factor должен быть больше 1, для уменьшения больше 0 и меньше 1</param>
        private void changeScale(float factor)
        {
            MyVector leftTop = new MyVector(); 
            MyVector rightBottom = new MyVector();
            getRect(leftTop, rightBottom);
            MyVector center = (leftTop + rightBottom) / 2;
            
            MyVector ray = leftTop - center;
            MyVector factorRay = (leftTop - center) * factor;
            MyVector testLeftTop = leftTop + factorRay - ray;

            ray = rightBottom - center;
            factorRay = (rightBottom - center) * factor;
            MyVector testRightBottom = rightBottom + factorRay - ray;

            if (isNotCollision(testLeftTop, testRightBottom) &&
                testRightBottom.X - leftTop.X > 50 && testRightBottom.Y - leftTop.Y > 50)
            {
                for (int i = 0; i < storage.size; ++i)
                {
                    ray = new MyVector(storage.getObject(i).x, storage.getObject(i).y) - center;
                    factorRay = (new MyVector(storage.getObject(i).x, storage.getObject(i).y) - center) * factor;
                    MyVector direction =  factorRay - ray;
                    if (storage.getObject(i).isActive)
                    {
                        storage.getObject(i).move(direction);
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

        /// <summary>
        /// Получает координаты верхнего левого и правого нижнего угла окружающего фигуру или группу фигур прямоугольника
        /// </summary>
        /// <param name="leftTop">Координаты верхнего левого угла окружающего прямоугольника</param>
        /// <param name="rightBottom">Координаты нижнего правого угла окружающего прямоугольника</param>
        private void getRect(MyVector leftTop, MyVector rightBottom)
        {
            storage.getObject(0).getRect(leftTop, rightBottom);
            MyVector curLeftTop = new MyVector();
            MyVector curRightBottom = new MyVector();
            for (int i = 1; i < storage.size; ++i)
            {
                Figure curElem = storage.getObject(i);
                curElem.getRect(curLeftTop, curRightBottom);
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

        /// <summary>
        /// Выходит ли прямоугольник с координатами верхнего левого и нижнего правого угла за границы области для рисования
        /// </summary>
        /// <param name="leftTop">Координаты верхнего левого угла окружающего прямоугольника</param>
        /// <param name="rightBottom">Координаты нижнего правого угла окружающего прямоугольника</param>
        /// <returns>Выводит true, если пересечения нет, false, если есть</returns>
        private bool isNotCollision(MyVector leftTop, MyVector rightBottom)
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
            MyVector leftTop = new MyVector();
            MyVector rightBottom = new MyVector();
            if (isMove)
            {
                getRect(leftTop, rightBottom);
                int dX = e.Location.X - lastMouseCoords.X;
                int dY = e.Location.Y - lastMouseCoords.Y;
                MyVector direction = new MyVector(dX, dY);
                if (isNotCollision(leftTop + direction, rightBottom + direction))
                {
                    for (int i = 0; i < storage.size; ++i)
                    {
                        if (storage.getObject(i).isActive)
                        {
                            storage.getObject(i).move(direction);
                        }
                    }
                }
                PaintBox.Refresh();
            }
            lastMouseCoords.changeCoords(e.Location);
        }

        private void PaintBox_Resize(object sender, EventArgs e)
        {
            rightBottomPaintBox.changeCoords(PaintBox.Size);
            for (int i = 0; i < storage.size; ++i)
            {
                MyVector leftTop = new MyVector();
                MyVector rightBottom = new MyVector();
                storage.getObject(i).getRect(leftTop, rightBottom);
                if (!isNotCollision(leftTop, rightBottom))
                {
                    storage.pop(i);
                    --i;
                }
            }
            PaintBox.Refresh();
        }
    }
}