using Flexy.AssetRefs.Extra;

namespace Flexy.UI
{
	public class BindableAppInfo : BindableBehaviour
	{
		[Bindable] public	String		AppName				=> Application.productName;
		[Bindable] public	String		AppBundle			=> Application.identifier;
		[Bindable] public	String		AppCompany			=> Application.companyName;
		[Bindable] public	String		AppVersion			=> Application.version;
		[Bindable] public	String		AppRev				=> AppInfo.AppRev;
		[Bindable] public	String		AppRevShort			=> AppInfo.AppRevShort;
		[Bindable] public	String		AppBranch			=> AppInfo.AppBranch;
		[Bindable] public	String		AppVersionTag		=> AppInfo.AppVersionTag;
		
		[Bindable] public	String		AppTagAndVersion	=> AppInfo.AppVersionTagAndVersion;
		[Bindable] public	String		AppRevAndBranch		=> AppInfo.RevisionAndBranch;
	}
}