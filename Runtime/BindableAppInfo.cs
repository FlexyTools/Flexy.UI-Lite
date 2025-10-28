namespace Flexy.UI
{
	public class BindableAppInfo : BindableBehaviour
	{
		private String? _cachedAppRev;
		
		[Bindable] public	String			AppName				=> Application.productName;
		[Bindable] public	String			AppBundle			=> Application.identifier;
		[Bindable] public	String			AppCompany			=> Application.companyName;
		[Bindable] public	String			AppVersion			=> Application.version;
		[Bindable] public	String			AppRev				=> _cachedAppRev ??= Resources.Load<TextAsset>( "Fun.Flexy/BuildArtifacts/Revision" ) is { text: not null } o ? o.text : "";
		
		public				void			SetAppRevision		( String revision ) => _cachedAppRev = revision;
	}
}