# AspNetCore.Rendertron
Galamai.Extensions

## NuGet packages
`Install-Package Galamai.Extensions.Context`

## Use Galamai.Extensions.Context

private async Task MethodAsync()
{
	var name = "name";

    using (OperationContext.Set(name, "value"))
    {
        await Task.Run(() =>
		{
		    var value = OperationContext.Get<string>(name); // "value"
	    });
    }
}
