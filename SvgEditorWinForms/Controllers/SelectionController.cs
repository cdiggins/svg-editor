using SvgDemoWinForms;

namespace SvgEditorWinForms
{
    public class SelectionController
    {
        public event EventHandler SelectionChanged;

        public List<ElementModel> SelectedShapes { get; private set; } = new(); 

        public void ClearSelection()
        {
            SelectedShapes.Clear();
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddToSelection(ElementModel shape)
        {
            AddToSelection(new List<ElementModel>() { shape });
        }

        public void AddToSelection(IEnumerable<ElementModel> shapes)
        {
            SelectedShapes.AddRange(shapes);
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateSelection(ElementModel element)
        {
            UpdateSelection(new List<ElementModel>() { element });
        }

        public void UpdateSelection(IEnumerable<ElementModel> elements)
        {
            SelectedShapes = elements.ToList();
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
