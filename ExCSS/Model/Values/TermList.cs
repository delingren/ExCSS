﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace ExCSS
{
    public class TermList : Term
    {
        private readonly List<GrammarSegment> _separator = new List<GrammarSegment>();
        private readonly List<Term> _items = new List<Term>();
        private const GrammarSegment DefaultSeparator = GrammarSegment.Comma;

        public TermList()
        {
        }

        public TermList(params Term[] terms)
        {
            for(var i = 0; i < terms.Length; ++i)
            {
                AddTerm(terms[i]);
                if(i != terms.Length-1)
                {
                    AddSeparator(DefaultSeparator);
                }
            }
        }

        public void AddTerm(Term term)
        {
            _items.Add(term);
        }

        internal void AddSeparator(GrammarSegment termSepertor)
        {
            _separator.Add(termSepertor);
        }

        public int Length
        {
            get { return _items.Count; }
        }

        [IndexerName("ListItems")]
        public Term this [int index]
        {
            //return index >= 0 && index < _items.Count ? _items[index] : null; 
            get { return _items[index]; }
        }

        public Term Item(int index)
        {
            return this[index];
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (var i = 0; i < _items.Count; i++)
            {
                builder.Append(_items[i]);

                if (_separator.Count - 1 < i) continue;

                switch (_separator[i])
                {
                    case GrammarSegment.Whitespace:
                        builder.Append(" ");
                        break;

                    case GrammarSegment.Comma:
                        builder.Append(",");
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// exposed enumeration for the adding of separators into term lists
        /// </summary>
        public enum TermSeparator
        {
            Comma,
            Space
        }
    }
}
