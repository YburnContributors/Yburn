namespace Yburn.UI
{
    public interface PlotParamForm
    {
        event PlotRequestEventHandler PlotRequest;
    }

    public delegate void PlotRequestEventHandler(
        object sender,
        PlotRequestEventArgs e
        );
}