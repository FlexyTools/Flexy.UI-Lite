namespace Flexy.UI
{
	public class	UIWidget :	BindableBehaviour	
	{
		private	State?	_state;
		
		public	State	State	=> _state == null ? _state = gameObject.GetComponentInParent<State>(true) : _state; 
	}
}