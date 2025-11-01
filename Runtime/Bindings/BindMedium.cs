namespace Flexy.UI.Bindings
{
	public class BindMedium : BindableBehaviour, IAutoSetup
	{
		[FormerlySerializedAs("ExpectedType")] 
		[SerializeField]	String	_expectedType = null!;

		[Bindable(TypeProvider=nameof(_expectedType))]
		public				Object?	DataObj {get;set;}

		public		void	Setup	( Object? data ) => DataObj = data;
	}
}