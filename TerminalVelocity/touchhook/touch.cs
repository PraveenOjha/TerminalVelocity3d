//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace TerVel
//{
//    public partial class touch : 
//    {
//        public touch()
//        {
//            InitializeComponent();
//            this.BackColor = Color. Transparent;
                 
//            Touchdown += OnTouchDownHandler;
//            Touchup += OnTouchUpHandler;
//            TouchMove += OnTouchMoveHandler;
            
//            Paint += new PaintEventHandler(this.OnPaintHandler);
//        }

//        private void OnPaintHandler(object sender, PaintEventArgs e)
//        {
//            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);

//        }

       

//        private void OnTouchDownHandler(object sender, EventArgs e)
//        {
//            if (e.IsPrimaryContact)
//            {
//                Main.pointerstate = 1;
//            }
          
//        }

     

//        private void OnTouchUpHandler(object sender, EventArgs e)
//        {
//            if (e.IsPrimaryContact)
//            {
//                Main.pointerstate = 0;
//            }
//        }

//        // Touch move event handler.
//        // in:
//        //      sender      object that has sent the event
//        //      e           touch event arguments
////Exercise3-Task2-Step1

//        private void OnTouchMoveHandler(object sender, EventArgs e)
//        {
//            // Find the stroke in the collection of the strokes in drawing.
//            if (e.IsPrimaryContact)
//            {
//             Main.pointerX=e.ContactX;
//             Main.pointerY=e.ContactY;
//            }
            
//        }


//    }
//}
