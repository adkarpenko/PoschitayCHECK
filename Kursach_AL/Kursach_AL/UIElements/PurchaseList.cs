﻿using System.Text;
using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Kursach_AL.Entities;
using System.Collections;

namespace Kursach_AL.UIElements
{
    public class PurchaseList : ScrollView, IEnumerable<PurchaseEntity>
    {
        public PurchaseList()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            Update();
        }

        void Update()
        {
            StackLayout sl = new StackLayout();
            foreach (PurchaseEntity p in App.list)
            {
                if (!p.shop.Contains("?"))
                {
                    Purchase purchase = new Purchase(p);
                    var tapGestureRecognizer = new TapGestureRecognizer();//жесты
                    tapGestureRecognizer.Tapped += purchase.Tap;
                    purchase.GestureRecognizers.Add(tapGestureRecognizer);//коллекция распознавателя жестов
                    sl.Children.Add(purchase);
                }
            }
            Content = sl;
        }

        public void Add(PurchaseEntity p)
        {
            App.list.Add(p);
            App.SaveContext();
            Update();
        }

        public void Remove(PurchaseEntity p)
        {
            App.list.Remove(p);
            App.SaveContext();
            Update();
        }

        public IEnumerator<PurchaseEntity> GetEnumerator()
        {
            return App.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return App.list.GetEnumerator();
        }
    }
}
