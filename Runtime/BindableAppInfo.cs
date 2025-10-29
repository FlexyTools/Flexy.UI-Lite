using Flexy.AssetRefs.Extra;

namespace Flexy.UI
{
	public class BindableAppInfo : BindableBehaviour
	{
		[Bindable] public	String			AppName				=> Application.productName;
		[Bindable] public	String			AppBundle			=> Application.identifier;
		[Bindable] public	String			AppCompany			=> Application.companyName;
		[Bindable] public	String			AppVersion			=> Application.version;
		[Bindable] public	String			AppRev				=> AppRevision.AppRev;
		[Bindable] public	String			AppRevShort			=> AppRevision.AppRevShort;
	}
}