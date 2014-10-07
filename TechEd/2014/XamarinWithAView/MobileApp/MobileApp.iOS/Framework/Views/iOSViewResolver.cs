﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MobileFramework.Shared.Contract;
using Xamarin.Forms;

namespace MobileApp.iOS.Framework.Views
{
    public class iOSViewResolver : IViewResolver
    {
        private readonly ILifetimeScope _scope;

        public iOSViewResolver(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public View ResolveView(object content)
        {
            var t = content.GetType();

            var typeName = t.FullName.Replace("ViewModel", "View");
            typeName = _filterTypeName(typeName);
            var nextToType = Type.GetType(string.Format("{0}, {1}", typeName, t.Assembly.FullName));

            var cell = _resolveView(nextToType);

            if (cell != null)
            {
                cell.BindingContext = content;

                return cell;
            }

            throw new Exception("Page could not be resolved: " + typeName);
        }

        public Page Resolve(object content)
        {
            var t = content.GetType();

            var typeName = t.FullName.Replace("ViewModel", "View");
            typeName = _filterTypeName(typeName);
            var nextToType = Type.GetType(string.Format("{0}, {1}", typeName, t.Assembly.FullName));

            var view = _resolve(nextToType);

            if (view == null)
            {
                //try doing one of it's base types
                var typeNameBase = t.BaseType.FullName.Replace("ViewModel", "View");
                typeNameBase = _filterTypeName(typeNameBase);
                var baseType = Type.GetType(string.Format("{0}, {1}", typeNameBase, t.BaseType.Assembly.FullName));
                view = _resolve(baseType);
            }

            if (view != null)
            {
                view.BindingContext = content;

                return view;
            }

            throw new Exception("Page could not be resolved: " + typeName);
        }

        string _filterTypeName(string typeName)
        {
            if (typeName.IndexOf("`") == -1)
            {
                return typeName;
            }
            var result = typeName.Substring(0, typeName.IndexOf("`"));
            return result;

        }

        Page _resolve(Type t)
        {
            if (t == null)
            {
                return null;
            }
            if (_scope.IsRegistered(t))
            {
                var tView = _scope.Resolve(t) as Page;

                return tView;
            }

            return null;
        }

        View _resolveView(Type t)
        {
            if (t == null)
            {
                return null;
            }

            if (_scope.IsRegistered(t))
            {
                var tView = _scope.Resolve(t) as View;
                return tView;
            }

            return null;
        }
    }
}
