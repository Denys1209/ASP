namespace MovieCatalog.MVCView.ViewModels.Shared;

public abstract class ModalViewModel
{
    public abstract string Id { get; }
    public abstract string HeaderId { get; }

    public virtual string ControllerName => string.Empty;
    public virtual string ActionName => string.Empty;
    
    public string Header { get; }
    public string PartialViewName { get; }
    public object Data { get; }
    
    protected ModalViewModel(
        string header,
        string partialViewName,
        object data,
        bool isInfoModal = false)
    {
        Header = header;
        PartialViewName = partialViewName;
        Data = data;
        IsInfoModal = isInfoModal;
    }
    
    public bool IsInfoModal { get; }
    
    public virtual IDictionary<string, string> GetRouteValues()
    {
        return new Dictionary<string, string>();
    }
}