using Nori;
using System.ComponentModel;
namespace NoriPoly;

class PolyStr : INotifyPropertyChanged {
   public string Str { get; private set; } = string.Empty;
   public Poly? Poly { get; private set; }

   public void UpdateStr (string str) {
      Str = str;
      try {
         Poly = Poly.Parse (str);
      } catch { return; }
      PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (null));
   }

   public event PropertyChangedEventHandler? PropertyChanged;
}

class PolyScene : Scene2 {
   public PolyScene (PolyStr str) {
      mStr = str;
      mStr.PropertyChanged += delegate { ZoomExtents (); };
      BgrdColor = Color4.Gray (216);
      Bound = new Bound2 (-100, -100, 100, 100);
      Root = new PolyVN (str);
   }

   public override void ZoomExtents () {
      Bound = new Bound2 (-100, -100, 100, 100);
      if (mStr.Poly != null) Bound = mStr.Poly.GetBound ().InflatedF (1.05);
      base.ZoomExtents ();
   }

   readonly PolyStr mStr;
}

class PolyVN : VNode {
   public PolyVN (PolyStr str) {
      mStr = str;
      mStr.PropertyChanged += delegate { Redraw (); };
   }

   public override void SetAttributes () =>
      Lux.Color = Color4.Blue;

   public override void Draw () {
      if (mStr.Poly == null) return;
      Lux.Poly (mStr.Poly);
   }

   readonly PolyStr mStr;
}
