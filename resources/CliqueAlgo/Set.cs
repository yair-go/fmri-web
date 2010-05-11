using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqueAlgo
{
    /// <summary>
    /// Set class
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public class Set<TElement> : ICollection<TElement>
    {
        private List<TElement> internalList = new List<TElement>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Set&lt;TElement&gt;"/> class.
        /// </summary>
        public Set()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set&lt;TElement&gt;"/> class.
        /// </summary>
        /// <param name="elements">The elements.</param>
        public Set(params TElement[] elements)
        {
            AddRange(elements);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="range">The range.</param>
        public void AddRange(IEnumerable<TElement> range)
        {
            foreach (TElement element in range)
                Add(element);
        }

        /// <summary>
        /// Unions the specified set.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        public Set<TElement> Union(Set<TElement> set)
        {
            Set<TElement> result = new Set<TElement>();

            result.AddRange(this);
            result.AddRange(set);

            return result;
        }

        /// <summary>
        /// Intersects the specified set.
        /// </summary>
        /// <param name="set">The set.</param>
        public Set<TElement> Intersect(Set<TElement> set)
        {
            Set<TElement> result = new Set<TElement>();

            foreach (TElement element in set)
                if (Contains(element))
                    result.Add(element);

            return result;
        }

        /// <summary>
        /// Differences the specified set.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        public Set<TElement> Difference(Set<TElement> set)
        {
            Set<TElement> result = new Set<TElement>();

            foreach (TElement element in set)
                if (!Contains(element))
                    result.Add(element);

            return result;
        }


        /// <summary>
        /// Gets or sets the <see cref="TElement"/> at the specified index.
        /// </summary>
        /// <value></value>
        public TElement this[int index]
        {
            get { return internalList[index]; }
            set { internalList[index] = value; }
        }

        #region ICollection<TElement> Members

        public void Add(TElement item)
        {
            if (!Contains(item))
                internalList.Add(item);
        }

        public void Clear()
        {
            internalList.Clear();
        }

        public bool Contains(TElement item)
        {
            return internalList.Contains(item);
        }

        public void CopyTo(TElement[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        public bool Remove(TElement item)
        {
            return internalList.Remove(item);
        }

        public int Count
        {
            get { return internalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        #endregion

        #region IEnumerable<TElement> Members

        IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        #endregion
    }
}
