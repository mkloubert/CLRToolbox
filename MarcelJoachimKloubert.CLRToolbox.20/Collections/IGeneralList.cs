// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections
{
    /// <summary>
    /// Describes a general list that can access items strong casted / converted.
    /// </summary>
    public interface IGeneralList : IList, IEnumerable<object>, ICloneable
    {
        #region Data Members (2)

        /// <summary>
        /// Gets if the list is empty or not.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets if the list is not empty or is empty.
        /// </summary>
        bool IsNotEmpty { get; }

        #endregion Data Members

        #region Operations (37)

        /// <summary>
        /// Adds a list of items.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" /> is <see langword="null" />.
        /// </exception>
        void AddRange(IEnumerable items);
        
        /// <summary>
        /// Adds a list of items of a specific type which are not null.
        /// </summary>
        /// <typeparam name="T">type of items to filter from <paramref name="items" />.</typeparam>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items" /> is <see langword="null" />.
        /// </exception>
        void AddRangeOf<T>(IEnumerable items);

        /// <summary>
        /// Returns that list as generic list.
        /// </summary>
        /// <returns>
        /// That list as generic object list. If that list has already the result type of
        /// that method the instance of that object is returned.
        /// </returns>
        IList<object> AsList();
        
        /// <summary>
        /// Returns that list as generic list.
        /// </summary>
        /// <typeparam name="T">Target type of the elements.</typeparam>
        /// <returns>
        /// That list as generic object list. If that list has already the result type of
        /// that method the instance of that object is returned.
        /// </returns>
        IList<T> AsList<T>();

        /// <summary>
        /// Casts the items of that list and return them casted sequence.
        /// </summary>
        /// <typeparam name="T">Target type of the items.</typeparam>
        /// <returns>The sequence with casted items.</returns>
        IEnumerable<T> Cast<T>();

        /// <summary>
        /// Clones that list.
        /// </summary>
        /// <returns>The cloned list.</returns>
        new IGeneralList Clone();

        /// <summary>
        /// Invokes an action for the items of that list by using casted versions of each item.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Only that items are taken which can be catsed to target type.
        /// </remarks>
        void ForEach<T>(Action<T, int> action);

        /// <summary>
        /// Invokes an action for the items of that list by using casted versions of each item.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="takeAll">
        /// Take all items of that list or only that one that can be casted to target type.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        void ForEach<T>(Action<T, int> action, bool takeAll);

        /// <summary>
        /// Invokes an action for the items of that list by using casted versions of each item.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <typeparam name="TState">
        /// Type of the state object for <paramref name="action" />.
        /// </typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">
        /// The state object for <paramref name="action" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Only that items are taken which can be catsed to target type.
        /// </remarks>
        void ForEach<T, TState>(Action<T, int, TState> action, TState actionState);

        /// <summary>
        /// Invokes an action for the items of that list by using casted versions of each item.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <typeparam name="TState">
        /// Type of the state object for <paramref name="action" />.
        /// </typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">
        /// The state object for <paramref name="action" />.
        /// </param>
        /// <param name="takeAll">
        /// Take all items of that list or only that one that can be casted to target type.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        void ForEach<T, TState>(Action<T, int, TState> action, TState actionState, bool takeAll);

        /// <summary>
        /// Invokes an action for the items of that list by using casted versions of each item.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <typeparam name="TState">
        /// Type of the state object for <paramref name="action" />.
        /// </typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">
        /// The function that produdes the state object for <paramref name="action" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> and/or <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Only that items are taken which can be catsed to target type.
        /// </remarks>
        void ForEach<T, TState>(Action<T, int, TState> action, Func<int, TState> actionStateFactory);

        /// <summary>
        /// Invokes an action for the items of that list by using casted versions of each item.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <typeparam name="TState">
        /// Type of the state object for <paramref name="action" />.
        /// </typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">
        /// The function that produdes the state object for <paramref name="action" />.
        /// </param>
        /// <param name="takeAll">
        /// Take all items of that list or only that one that can be casted to target type.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> and/or <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        void ForEach<T, TState>(Action<T, int, TState> action, Func<int, TState> actionStateFactory, bool takeAll);

        /// <summary>
        /// Returns an item of that list casted / converted.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="index">The index of the item inside that list.</param>
        /// <returns></returns>
        T GetItem<T>(int index);

        /// <summary>
        /// Creates a new object from the items of that list.
        /// </summary>
        /// <typeparam name="TResult">Type of the result object.</typeparam>
        /// <param name="func">The function that is used to create the result object.</param>
        /// <returns>The created object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        TResult Materialize<TResult>(Func<IGeneralList, TResult> func);

        /// <summary>
        /// Creates a new object from the items of that list.
        /// </summary>
        /// <typeparam name="TState">The type of the object that is additionally used to build the result object.</typeparam>
        /// <typeparam name="TResult">Type of the result object.</typeparam>
        /// <param name="func">The function that is used to create the result object.</param>
        /// <param name="funcState">
        /// The state object for <paramref name="func" />.
        /// </param>
        /// <returns>The created object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        TResult Materialize<TState, TResult>(Func<IGeneralList, TState, TResult> func, TState funcState);

        /// <summary>
        /// Creates a new object from the items of that list.
        /// </summary>
        /// <typeparam name="TState">The type of the object that is additionally used to build the result object.</typeparam>
        /// <typeparam name="TResult">Type of the result object.</typeparam>
        /// <param name="func">The function that is used to create the result object.</param>
        /// <param name="funcStateFactory">
        /// The function that is used to build the state object for <paramref name="func" />.
        /// </param>
        /// <returns>The created object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> and/or <paramref name="funcStateFactory" /> are <see langword="null" />.
        /// </exception>
        TResult Materialize<TState, TResult>(Func<IGeneralList, TState, TResult> func, Func<IGeneralList, TState> funcStateFactory);

        /// <summary>
        /// Filteres out the items from that list that has a specific type.
        /// </summary>
        /// <typeparam name="T">Target type of the items.</typeparam>
        /// <returns>The filtered sequence of items.</returns>
        IEnumerable<T> OfType<T>();

        /// <summary>
        /// Returns a randomized sequence of the items of that list.
        /// </summary>
        /// <returns>The randomized list of items.</returns>
        IEnumerable<object> Randomize();

        /// <summary>
        /// Returns a randomized sequence of the items of that list.
        /// </summary>
        /// <param name="rand">The randomizer to use.</param>
        /// <returns>The randomized list of items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rand" /> is <see langword="null" />.
        /// </exception>
        IEnumerable<object> Randomize(Random rand);

        /// <summary>
        /// Removes a list of items.
        /// </summary>
        /// <param name="index">The start index.</param>
        /// <param name="count">The number of items to remove.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="index" /> and <paramref name="count" /> do not represent a valid
        /// area inside that list.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is invalid.
        /// </exception>
        void RemoveRange(int index, int count);

        /// <summary>
        /// Reverses the order of the elements in that list.
        /// </summary>
        void Reverse();

        /// <summary>
        /// Reverses the order of the elements in that list in the specific range.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to reverse.</param>
        /// <param name="count">The number of elements in the range to reverse.</param>
        /// <paramref name="index" /> and <paramref name="count" /> do not represent a valid
        /// area inside that list.
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> and/or <paramref name="count" /> are invalid.
        /// </exception>
        void Reverse(int index, int count);

        /// <summary>
        /// Returns the items of that list as sequence of casted objects by using a selector delegate.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="selector">The function that casts each element.</param>
        /// <returns>The list of casted items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        IEnumerable<T> Select<T>(Func<object, int, T> selector);

        /// <summary>
        /// Returns the items of that list as sequence of casted objects by using a selector delegate.
        /// </summary>
        /// <typeparam name="TState">Type of the state object for <paramref name="selector" />.</typeparam>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="selector">The function that casts each element.</param>
        /// <param name="selectState">
        /// The state object for <paramref name="selector" />.
        /// </param>
        /// <returns>The list of casted items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        IEnumerable<T> Select<TState, T>(Func<object, int, TState, T> selector, TState selectState);

        /// <summary>
        /// Returns the items of that list as sequence of casted objects by using a selector delegate.
        /// </summary>
        /// <typeparam name="TState">Type of the state object for <paramref name="selector" />.</typeparam>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="selector">The function that casts each element.</param>
        /// <param name="selectStateFactory">
        /// The function that produces the state object for <paramref name="selector" />.
        /// </param>
        /// <returns>The list of casted items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selector" /> and/or <paramref name="selectStateFactory" /> are <see langword="null" />.
        /// </exception>
        IEnumerable<T> Select<TState, T>(Func<object, int, TState, T> selector, Func<object, int, TState> selectStateFactory);

        /// <summary>
        /// Shuffles the items of that list to new positions.
        /// </summary>
        void Shuffle();

        /// <summary>
        /// Shuffles the items of that list to new positions.
        /// </summary>
        /// <param name="rand">The randomizer to use.</param>
        /// <returns>The randomized list of items.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rand" /> is <see langword="null" />.
        /// </exception>
        void Shuffle(Random rand);

        /// <summary>
        /// Sorts the items of that list.
        /// </summary>
        void Sort();

        /// <summary>
        /// Returns that list as a new array.
        /// </summary>
        /// <returns>That list as new array.</returns>
        object[] ToArray();

        /// <summary>
        /// Returns that list as a new array and casts each item to a target type.
        /// </summary>
        /// <typeparam name="T">Target type of the items.</typeparam>
        /// <returns>That list as new array.</returns>
        T[] ToArray<T>();

        /// <summary>
        /// Returns that list as a new array and casts each item to a target type.
        /// </summary>
        /// <typeparam name="T">Target type of the items.</typeparam>
        /// <param name="ofType">
        /// Filter out items (<see langword="true" />) or cast them (<see langword="false" />).
        /// </param>
        /// <returns>That list as new array.</returns>
        T[] ToArray<T>(bool ofType);

        /// <summary>
        /// Returns that list as a new generic list.
        /// </summary>
        /// <returns>The new list.</returns>
        IList<object> ToList();

        /// <summary>
        /// Returns that list as a new generic list.
        /// </summary>
        /// <typeparam name="T">The target type of the items.</typeparam>
        /// <returns>The new list.</returns>
        IList<T> ToList<T>();

        /// <summary>
        /// Returns that list as a new generic list.
        /// </summary>
        /// <typeparam name="T">The target type of the items.</typeparam>
        /// <param name="ofType">
        /// Filter out items (<see langword="true" />) or cast them (<see langword="false" />).
        /// </param>
        /// <returns>The new list.</returns>
        IList<T> ToList<T>(bool ofType);

        /// <summary>
        /// Transforms each item of that list to a new type.
        /// </summary>
        /// <typeparam name="TNew">The target type.</typeparam>
        void Transform<TNew>();

        /// <summary>
        /// Transforms each item of that list to a new type.
        /// </summary>
        /// <typeparam name="TNew">The target type.</typeparam>
        /// <param name="transformer">The function to use to transform an object of that list.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="transformer" /> is <see langword="null" />.
        /// </exception>
        void Transform<TNew>(Func<object, int, TNew> transformer);

        /// <summary>
        /// Transforms each item of that list to a new type.
        /// </summary>
        /// <typeparam name="TNew">The target type.</typeparam>
        /// <typeparam name="TState">
        /// Type of the optional state object for <paramref name="transformer" />.
        /// </typeparam>
        /// <param name="transformer">The function to use to transform an object of that list.</param>
        /// <param name="transState">
        /// The optional state object for <paramref name="transformer" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="transformer" /> is <see langword="null" />.
        /// </exception>
        void Transform<TState, TNew>(Func<object, int, TState, TNew> transformer, TState transState);

        /// <summary>
        /// Transforms each item of that list to a new type.
        /// </summary>
        /// <typeparam name="TNew">The target type.</typeparam>
        /// <typeparam name="TState">
        /// Type of the optional state object for <paramref name="transformer" />.
        /// </typeparam>
        /// <param name="transformer">The function to use to transform an object of that list.</param>
        /// <param name="transStateFactory">
        /// The function that produces the optional state object for <paramref name="transformer" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="transformer" /> and/or <paramref name="transStateFactory" /> are <see langword="null" />.
        /// </exception>
        void Transform<TState, TNew>(Func<object, int, TState, TNew> transformer, Func<object, int, TState> transStateFactory);

        #endregion Operations
    }
}