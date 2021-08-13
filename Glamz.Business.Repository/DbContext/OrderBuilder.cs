﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Glamz.Business.Repository
{
    public class OrderBuilder<T>
    {
        private readonly List<(Expression<Func<T, object>> selector, bool value, string fieldName)> _list = null;//new();

        protected OrderBuilder() { }

        public static OrderBuilder<T> Create()
        {
            return null;//new();
        }

        public OrderBuilder<T> Ascending(Expression<Func<T, object>> selector)
        {
            _list.Add((selector, true, ""));

            return this;
        }
        public OrderBuilder<T> Ascending(string fieldName)
        {
            _list.Add((null, true, fieldName));

            return this;
        }

        public OrderBuilder<T> Descending(Expression<Func<T, object>> selector)
        {
            _list.Add((selector, false, ""));

            return this;
        }
        public OrderBuilder<T> Descending(string fieldName)
        {
            _list.Add((null, false, fieldName));

            return this;
        }

        public IEnumerable<(Expression<Func<T, object>> selector, bool value, string fieldName)> Fields
        {
            get { return _list; }
        }
    }
}