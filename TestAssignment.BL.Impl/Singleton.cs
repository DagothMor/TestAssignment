using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using TestAssignment.BL.Abstraction;

namespace TestAssignment.BL.Impl
{
	public class Singleton : IElementMerger
	{
		private static Singleton instance;

		private Singleton()
		{

		}

		public static Singleton Get()
		{
			if (instance == null)
				instance = new Singleton();
			return instance;
		}

		public IEnumerable<IElement> MergeElements(IEnumerable<IElement> elements, IElement newElement)
		{
			var sortedElements = elements.OrderBy(x => x.Number).ToList();
			var listout = new List<IElement>();
			int index = 0;
			bool ifInsertedNewElement = false;
			foreach (var element in sortedElements)
			{
				if (ifInsertedNewElement == false)
				{
					if (index++ == newElement.Number)
					{
						ifInsertedNewElement = true;
						listout.Add(newElement);
						element.Number += 1;
						listout.Add(element);
					}
					else
					{
						listout.Add(element);
						index++;
					}
				}
				else
				{
					if (element.Number == index)
					{
						element.Number += 1;
						listout.Add(element);
						index++;
					}
					else
					{
						ifInsertedNewElement = false;
						listout.Add(element);
						index++;
					}
				}
			}
			return listout;
		}
	}
}
