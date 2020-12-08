using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using NoOpRunner.Core;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace NoOpRunner.Client
{
    public partial class FlyweightTestOPP : Window
    {
        public FlyweightTestOPP()
        {
            InitializeComponent();

            
            Stopwatch stopwatch = new Stopwatch();
            
            GC.Collect();
            GC.Collect();
            GC.Collect();
            
            var startMemory = GC.GetTotalMemory(false);
            
            stopwatch.Start();
            
            InitCanvasChildrens();
            
            stopwatch.Stop();
            var endMemory = GC.GetTotalMemory(false);
            
            Console.WriteLine("Object creation time elapsed: "+stopwatch.Elapsed.Milliseconds);
            Console.WriteLine("Object creation memory used: "+ ((endMemory-startMemory)/1024)+"KB"+" or "+((endMemory-startMemory)/1024/1024)+"MB");

            PushAllChildrenToPool();
                
            stopwatch.Reset();
            
            GC.Collect();
            GC.Collect();
            GC.Collect();
            
            startMemory = GC.GetTotalMemory(false);
            
            stopwatch.Start();

            PopAllChildrenToCanvas();
            
            stopwatch.Stop();
            endMemory = GC.GetTotalMemory(false);
            
            Console.WriteLine("Flyweight time elapsed: "+stopwatch.Elapsed.Milliseconds);
            Console.WriteLine("Flyweight memory used: "+ ((endMemory-startMemory)/1024)+"KB"+" or "+((endMemory-startMemory)/1024/1024)+"MB");
        }

        private void InitCanvasChildrens()
        {
            flyweight_test.Children.Clear();
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    var rec = new Rectangle()
                    {
                        Height = 20,
                        Width = 20,
                        Fill = System.Windows.Media.Brushes.Red
                    };
                    
                    flyweight_test.Children.Add(rec);
                    
                    Canvas.SetTop(rec, i*20);
                    Canvas.SetLeft(rec, j*20);
                }
            }
        }

        private void PushAllChildrenToPool()
        {
            foreach (UIElement child in flyweight_test.Children)
            {
                DisposedObjectsPool.Push(child);
            }
            
            flyweight_test.Children.Clear();
        }

        private void PopAllChildrenToCanvas()
        {
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    Rectangle rec;

                    if (DisposedObjectsPool.Contains<Rectangle>())
                    {
                        rec = DisposedObjectsPool.Pop<Rectangle>();
                    }
                    else
                    {
                        rec = new Rectangle()
                        {
                            Height = 20,
                            Width = 20,
                            Fill = System.Windows.Media.Brushes.Red
                        }; 
                    }
                    
                    flyweight_test.Children.Add(rec);
                    
                    Canvas.SetTop(rec, i*20);
                    Canvas.SetLeft(rec, j*20);
                }
            }
        }
    }
}