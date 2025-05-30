using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	[Serializable]
	public class ConstraintPositionOffset : Constraint
	{
		public Vector3 offset;

		private Vector3 defaultLocalPosition;

		private Vector3 lastLocalPosition;

		private bool initiated;

		private bool positionChanged => transform.localPosition != lastLocalPosition;

		public override void UpdateConstraint()
		{
			if (!(weight <= 0f) && base.isValid)
			{
				if (!initiated)
				{
					defaultLocalPosition = transform.localPosition;
					lastLocalPosition = transform.localPosition;
					initiated = true;
				}
				if (positionChanged)
				{
					defaultLocalPosition = transform.localPosition;
				}
				transform.localPosition = defaultLocalPosition;
				transform.position += offset * weight;
				lastLocalPosition = transform.localPosition;
			}
		}

		public ConstraintPositionOffset()
		{
		}

		public ConstraintPositionOffset(Transform transform)
		{
			base.transform = transform;
		}
	}
}
