﻿using Newtonsoft.Json;

namespace SvgDemoWinForms
{
    public class ElementModel
    {
        public string Name { get; set; } 
        public string Id { get; set; }

        public ElementModel()
        {
            Id = new object().GetHashCode().ToString();
            Name = GetType().Name;
        }
    }

    public class GroupModel : ElementModel
    {
        public List<ElementModel> Elements { get; set; }
            = new ();
    }

    public class DefModel : ElementModel
    { }

    public class ColorServerModel : DefModel
    {
        public double Opacity { get; set; }
    }

    public class SolidColorModel : ColorServerModel
    {
        public ColorModel Color { get; set; }
    }

    public class GradientStopModel
    {
        public double Offset { get; set; } 
        public ColorModel Color { get; set; } = ColorModel.Black;
        public double Opacity { get; set; } = 1;

        // https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/transform
        //public Transform Transform;
    }

    public class LinearGradientModel : ColorServerModel
    {
        public List<GradientStopModel> Stops { get; set; } = new();
    }

    public class LineModel : SimpleShapeModel
    {
        public PointModel Start { get; set; }
        public PointModel End { get; set; }
    }

    public enum LineCapEnum
    {
        Butt,
        Square,
        Round,
    }

    public enum LineJoinEnum
    {
        Miter,
        Round,
        Bevel,
    }

    public class ColorModel
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public static ColorModel Black => new() { R = 0, G = 0, B = 0 };
        public static ColorModel Red => new() { R = 255, G = 0, B = 0 };
        public static ColorModel Green => new() { R = 0, G = 255, B = 0 };
        public static ColorModel Blue => new() { R = 0, G = 0, B = 255 };
        public static ColorModel White => new() { R = 255, G = 255, B = 255 };
    }

    public class PointModel
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class SizeModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class ShapeModel : ElementModel
    {
        public ColorModel StrokeColor { get; set; } = ColorModel.Black;
        public double StrokeWidth { get; set; } = 3;
        public ColorModel FillColor { get; set; } = ColorModel.White;
    }

    public class SimpleShapeModel : ShapeModel
    {
        public double Left => Math.Min(Position.X, Position.X + Size.Width);
        public double Right => Math.Max(Position.X, Position.X + Size.Width);
        public double Top => Math.Min(Position.Y, Position.Y + Size.Height);
        public double Bottom => Math.Max(Position.Y, Position.Y + Size.Height);

        public PointModel Position { get; set; } = new ();
        public SizeModel Size { get; set; } = new ();

        public double Width => Right - Left;
        public double Height => Bottom - Top;
        public double CenterX => Left + Width / 2;
        public double CenterY => Top + Height / 2;
        public double Radius => Math.Min(Width, Height) / 2;
    }

    public class EllipseModel : SimpleShapeModel
    {
    }

    public class RectModel : SimpleShapeModel
    {
    }

    public class CircleModel : SimpleShapeModel
    {
    }

    public class SquareModel : SimpleShapeModel
    {
    }

    public class PolyLineModel : ShapeModel
    {
        public List<PointModel> Points { get; set; } = new();
    }

    public class PolygonModel : ShapeModel
    {
        public List<PointModel> Points { get; set; } = new();
    }

    public class TextModel : ElementModel
    {
        public string Family { get; set; } = "Arial";
        public string Style { get; set; } = "";
        public string Text { get; set; } = "";
        public double Size { get; set; } = 12;
    }

    public class Viewport
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class Settings
    {
        public List<string> RecentFiles { get; set; }
    }

    public class DocumentModel : GroupModel
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, 
                Formatting.Indented, 
                new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        public static DocumentModel? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<DocumentModel>(json,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
        }

        public DocumentModel Clone()
        {
            return FromJson(ToJson());
        }

        public IEnumerable<ElementModel> AllElements()
        {
            return Elements.SelectMany(e => e.SelfAndDescendants());
        }

        public void Remove(ElementModel e)
        {

            if (Elements.Contains(e))
            {
                Elements.Remove(e);
            }
        }
    }

    public static class ElementModelExtensions
    {
        public static IEnumerable<ElementModel> SelfAndDescendants(this ElementModel self)
        {
            yield return self;
            if (self is GroupModel g)
            {
                foreach (var x in g.Elements)
                    foreach (var y in x.SelfAndDescendants())
                        yield return y;
            }
        }

        public static void Remove(this ElementModel self, ElementModel model)
        {
            if (self is GroupModel g)
            {
                g.Elements.Remove(model);
                foreach (var e in g.Elements)
                    e.Remove(model);
            }
        }
    }
}
