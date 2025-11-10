using System;
using System.Linq;
using UnityEngine;

namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Enum))]
	public class BehaviourEnumBinder : ParamsEnumBinder
	{
		[SerializeField]	BindPair[]	_stateObjects;

		public override		String		ParamsArrayFieldName => nameof(_stateObjects);

		protected override void Bind( Boolean init )
		{
			var val = _getter( );

			//DON'T CHANGE! Done on purpose for case when one behaviour set for two enum states
			{
				BindPair pair = _stateObjects.FirstOrDefault(x => x.EnumVal == val );
				Behaviour bValue = pair?.Value;

				foreach( var bindingPair in _stateObjects )
				{
					if( bindingPair.Value != null)
						bindingPair.Value.enabled = bindingPair.Value == bValue;
				}
					
			}
		}

		[Serializable]
		private class BindPair : EnumBindingPair<Behaviour>{ }
	}
}