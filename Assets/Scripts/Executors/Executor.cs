using UnityEngine;
using System;

namespace Executors
{
	public abstract class Executor : MonoBehaviour
	{
		public abstract void Execute(float signal);
	}
}
