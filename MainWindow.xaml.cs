using Nori;
using System.Windows;
namespace NoriPoly;

/// <summary>Interaction logic for MainWindow.xaml</summary>
public partial class MainWindow : Window {
   public MainWindow () {
      Lib.Init ();
      InitializeComponent ();
      mContent.Child = Lux.CreatePanel ();
      Lux.OnReady.Subscribe (OnLuxReady);
      mStr.TextChanged += delegate { mPolyStr.UpdateStr (mStr.Text); };
   }

   void OnLuxReady (int _) =>
      Lux.UIScene = new PolyScene (mPolyStr);

   readonly PolyStr mPolyStr = new ();
}
