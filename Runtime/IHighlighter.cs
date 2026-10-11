namespace dtfernandes.UINavigation
{
    public interface IHighlighter
    {
        void EnterHighlight();
        void ExitHighlight();
    }
    public interface ISelectableResponse : IHighlighter
    {
        void SetDisabled();
    }

}

