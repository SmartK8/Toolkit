﻿$if$ ($sharpdx_platform_wp8$ == true)namespace $safeprojectname$
{
    public partial class MainPage
    {
        private readonly $safeclassname$ game;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            game = new $safeclassname$();
            game.Run(DrawingSurface);
        }
    }
}
$else$using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace $safeprojectname$
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        private readonly $safeclassname$ game;
    
        public MainPage()
        {
            this.InitializeComponent();
            game = new $safeclassname$();
            this.Loaded += (sender, args) => game.Run(this);        
        }
    }
}
$endif$