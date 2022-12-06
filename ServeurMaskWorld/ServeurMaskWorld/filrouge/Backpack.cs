using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
	[Serializable]
	public class Backpack
    {
        private Stack<IObject> mStack = new Stack<IObject>();

        public Backpack()
        {

        }

        public int getSizePack()
        {
            return this.mStack.Count;
        }

		/*
		* put objects in the backpack at the top
		*/
		public void pack(IObject pObject)
		{
			this.mStack.Push(pObject);
		}

		/*
		* remove the top objects in the backpack 
		*/
		public IObject unPack()
		{
			var @object = this.mStack.Peek();
			this.mStack.Pop();
			return @object;
		}

		/*
		*  to see if the backpack is empty
		*/
		public bool isNotEmpty()
		{
			return this.mStack.Count > 0;
		}

	}
}
