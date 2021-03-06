﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManage
{
    [Serializable]
    public class Order : IComparable<Order>
    {
        public string orderTime, customeName, title;
        public uint orderNum;
        public double totalMoney, receivables, changeMoney;   //应收、实付、找零

        public string Title { get => title; set => title = value; }
        public string OrderTime { get => orderTime; set => orderTime = value; }
        public string CustomeName { get => customeName; set => customeName = value; }
        public uint OrderNum { get => orderNum; set => orderNum = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }
        public double Receivables { get => receivables; set => receivables = value; }
        public double ChangeMoney { get => changeMoney; set => changeMoney = value; }

        //存储订单明细
        public List<OrderItem> itemList = new List<OrderItem>();

        public Order() {
            DateTime time = DateTime.Now;
            this.orderTime = time.ToString();
            this.orderNum = uint.Parse(time.Day.ToString() + time.Hour.ToString() + time.Minute.ToString() + time.Second.ToString());
        }

        public Order(string title, string customeName, double receivables)
        {
            DateTime time = DateTime.Now;
            this.title = title;
            this.orderTime = time.ToString();
            this.customeName = customeName;
            this.orderNum = uint.Parse(time.Day.ToString() + time.Hour.ToString() + time.Minute.ToString() + time.Second.ToString());
            this.receivables = receivables;
            this.totalMoney = 0;
            this.changeMoney = 0;
        }

        //添加明细项
        public void AddItems(OrderItem item)
        {
            foreach (OrderItem oi in itemList)
            {
                if (item.Equals(oi))
                {
                    Console.WriteLine("添加失败！明细重复！");
                    return;
                }
            }
            item.Money = item.cargoNum * item.unitPrice;
            itemList.Add(item);
            totalMoney += item.Money;
            changeMoney = receivables - totalMoney;
            Console.WriteLine("添加成功！");
        }

        //重写Equals方法
        public override bool Equals(object obj)
        {
            Order m = obj as Order;
            return m != null && m.orderNum == orderNum && m.customeName == customeName;
        }

        public override string ToString()
        {
            string tmp = "项目：" + title + "\n" + "订单号：" + orderNum + "\n" + "时间：" + orderTime + "\t" + "姓名：" + customeName + "\n" + "应收：" + totalMoney + "\t" + "实付：" + receivables + "\t" + "找零：" + changeMoney + "\n" + "商品名称 ||数量 ||单价 ||金额" + "\n";
            foreach (OrderItem i in itemList)
            {
                tmp += i.ToString() + "\n";
            }
            return tmp;
        }

        public int CompareTo(Order other)
        {
            return this.orderNum.CompareTo(other.orderNum);
        }
    }
}
