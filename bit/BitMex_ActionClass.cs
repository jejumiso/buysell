using bit.Model;
using BitMEX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bit.BitMex_ActionClass
{
    public class BitMex_ActionClass
    {
        // jejuairfarm
        private static string bitmexKey = "I1mAR6Kn0HxzW6uRZS4pSFwq";
        private static string bitmexSecret = "Fmv_Kvyq663upBdsyOwvlt7Mmo1KlvbH7sG5HlV2s9Gv8AMk";


        BitMEXApi bitemex = new BitMEXApi(bitmexKey, bitmexSecret);

        double iniinitial_value = 0.0;
        string pre_timestamp = "";
        int no = 199;
        /// <summary>
        /// order_System2   version 2.0
        /// spring 주문을 굳이 나눠서 하지 않고 한번에 넣어야 좋을듯함...
        /// 이것도 나쁘지 않음...
        /// 초 급등에 대한 대응이 없음... 초 급등 할때도 소폭의 이익만 챙기게됨...
        /// spring의 적게주고 margin을 적게줌 : 안전하게 자주 먹는 시스템임...   아무리 그래도 step가 올라가면 어쩔수 없음..
        /// 급등을 먹을려고 하면   spring의 값을 넓게 주고 margin 값을 높이 줌   => 이것도 괜츦을듯   만들어보자..
        /// </summary>
        public void order_System2(List<bitmex_bucketed> bitemex_Bucketeds, bitemex_position bitemex_position)
        {

            //[1-1]  setting
            #region setting
            int step1_Qty; double step1_spring; double _margin1;          // 7025.0      ( 35,125,000)
            int step2_Qty; double step2_spring; double _margin2;         // 7037.5    ( 70,375,000)
            int step3_Qty; double step3_spring; double _margin3;       // 7068.75     (141,375,000)
            int step4_Qty; double step4_spring; double _margin4;
            int step5_Qty; double step5_spring; double _margin5;
            int step6_Qty; double step6_spring; double _margin6;
            int step7_Qty; double step7_spring; double _margin7;
            int step8_Qty; double step8_spring; double _margin8;
            int step9_Qty; double step9_spring; double _margin9;
            int step10_Qty; double step10_spring; double _margin10;

            step1_Qty = 1000; step1_spring = 3.0; _margin1 = 15.0;
            step2_Qty = 3000; step2_spring = 8.0; _margin2 = 15.0;
            step3_Qty = 3001; step3_spring = 15.0; _margin3 = 23.0;
            step4_Qty = 3002; step4_spring = 30.0; _margin4 = 40.0;
            step5_Qty = 3003; step5_spring = 30.0; _margin5 = 40.0;
            step6_Qty = 3004; step6_spring = 55.0; _margin6 = 60.0;
            step7_Qty = 3005; step7_spring = 80.0; _margin7 = 100.0;
            step8_Qty = 3006; step8_spring = 140.0; _margin8 = 160.0;
            step9_Qty = 3007; step9_spring = 180.0; _margin9 = 210.0;
            step10_Qty = 3008; step10_spring = 250.0; _margin10 = 250.0;

            // hyunju3414764
            //step1_Qty = 500; step1_spring = 5.0; _margin1 = 15.0;
            //step2_Qty = 500; step2_spring = 8.0; _margin2 = 15.0;
            //step3_Qty = 500; step3_spring = 15.0; _margin3 = 23.0;
            //step4_Qty = 400; step4_spring = 30.0; _margin4 = 40.0;
            //step5_Qty = 400; step5_spring = 55.0; _margin5 = 60.0;
            //step6_Qty = 400; step6_spring = 80.0; _margin6 = 100.0;
            //step7_Qty = 50; step7_spring = 140.0; _margin7 = 160.0;
            //step8_Qty = 50; step8_spring = 180.0; _margin8 = 210.0;
            //step9_Qty = 50; step9_spring = 250.0; _margin9 = 250.0;
            #endregion
            double standard_avgCostPrice = (double)bitemex_position.avgCostPrice;
            double modify_standard = 0;
            if (Math.Abs(bitemex_position.currentQty) > 200000)
            {
                modify_standard = 800;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 150000)
            {
                modify_standard = 250;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 130000)
            {
                modify_standard = 200;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 100000)
            {
                modify_standard = 130;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 90000)
            {
                modify_standard = 100;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 80000)
            {
                modify_standard = 90;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 70000)
            {
                modify_standard = 80;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 60000)
            {
                modify_standard = 70;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 50000)
            {
                modify_standard = 50;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 40000)
            {
                modify_standard = 40;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 30000)
            {
                modify_standard = 30;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 20000)
            {
                modify_standard = 20;
            }
            else if (Math.Abs(bitemex_position.currentQty) > 10000)
            {
                modify_standard = 15;
            }
            else
            {
                //empty modify_standard =0
            }
            if (bitemex_position.currentQty > 0)
            {
                standard_avgCostPrice = standard_avgCostPrice - modify_standard;
            }
            else
            {
                standard_avgCostPrice = standard_avgCostPrice + modify_standard;
            }



            //[1-2] setting
            iniinitial_value = bitemex_Bucketeds[0].open;

            //[2]
            #region 주문 넣기
            //[2-1] 기존 주문 삭제
            bitemex.DeleteAllOrders("{\"clOrdID\":\"" + (no - 1) + "\"}");
            //[2-2] 주문 넣기
            List<bitmex_order> list_bitmex_order = new List<bitmex_order>();
            bitmex_order _bitmex_order;
            double price = 0.0;

            

            double close = bitemex_Bucketeds[0].close;
            #region step1
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice,  close, iniinitial_value, step1_spring, step1_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step1_spring, step1_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step2
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step2_spring, step2_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step2_spring, step2_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step3
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step3_spring, step3_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step3_spring, step3_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step4
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step4_spring, step4_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step4_spring, step4_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step5
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step5_spring, step5_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step5_spring, step5_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step6
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step6_spring, step6_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step6_spring, step6_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step7
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step7_spring, step7_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step7_spring, step7_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step8
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step8_spring, step8_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step8_spring, step8_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step9
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step9_spring, step9_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step9_spring, step9_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion
            #region step10
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Sell", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step10_spring, step10_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            _bitmex_order = new bitmex_order();
            _bitmex_order = get_bitmex_order("Buy", bitemex_position, standard_avgCostPrice, close, iniinitial_value, step10_spring, step10_Qty);
            if (_bitmex_order != null)
            {
                list_bitmex_order.Add(_bitmex_order);
            }
            #endregion

            if (list_bitmex_order.Count() > 0)
            {
                string _result = bitemex.PostOrders_bulk(list_bitmex_order);
            }

            #endregion

            //[3]  청산
            if (no > 1)
            {
                list_bitmex_order = new List<bitmex_order>();
                List<bitmex_order> recent_orders = new List<bitmex_order>();

                string pre_clOrdID = (no - 1).ToString();
                var json_result = bitemex.GetOrders("XBTUSD", "", 100, true, "");
                recent_orders = JsonConvert.DeserializeObject<List<bitmex_order>>(json_result);

                foreach (var item in recent_orders.Where(p => p.clOrdID == (pre_clOrdID) && p.cumQty > 0))
                {
                    _bitmex_order = new bitmex_order();
                    string side = "";
                    if (item.side == "Buy")
                    {
                        side = "Sell";
                        if (item.cumQty <= step1_Qty)
                        {
                            price = item.price + _margin1;
                        }
                        else if (item.cumQty <= step2_Qty)
                        {
                            price = item.price + _margin2;
                        }
                        else if (item.cumQty <= step3_Qty)
                        {
                            price = item.price + _margin3;
                        }
                        else if (item.cumQty <= step4_Qty)
                        {
                            price = item.price + _margin4;
                        }
                        else if (item.cumQty <= step5_Qty)
                        {
                            price = item.price + _margin5;
                        }
                        else if (item.cumQty <= step6_Qty)
                        {
                            price = item.price + _margin6;
                        }
                        else if (item.cumQty <= step7_Qty)
                        {
                            price = item.price + _margin7;
                        }
                        else if (item.cumQty <= step8_Qty)
                        {
                            price = item.price + _margin8;
                        }
                        else if (item.cumQty <= step9_Qty)
                        {
                            price = item.price + _margin9;
                        }
                        else
                        {
                            price = item.price + _margin10;
                        }
                    }
                    else
                    {
                        side = "Buy";
                        if (item.cumQty <= step1_Qty)
                        {
                            price = item.price - _margin1;
                        }
                        else if (item.cumQty <= step2_Qty)
                        {
                            price = item.price - _margin2;
                        }
                        else if (item.cumQty <= step3_Qty)
                        {
                            price = item.price - _margin3;
                        }
                        else if (item.cumQty <= step4_Qty)
                        {
                            price = item.price - _margin4;
                        }
                        else if (item.cumQty <= step5_Qty)
                        {
                            price = item.price - _margin5;
                        }
                        else if (item.cumQty <= step6_Qty)
                        {
                            price = item.price - _margin6;
                        }
                        else if (item.cumQty <= step7_Qty)
                        {
                            price = item.price - _margin7;
                        }
                        else if (item.cumQty <= step8_Qty)
                        {
                            price = item.price - _margin9;
                        }
                        else if (item.cumQty <= step9_Qty)
                        {
                            price = item.price - _margin9;
                        }
                        else
                        {
                            price = item.price - _margin10;
                        }

                    }


                    if (recent_orders.Where(p => p.side == side && p.price == price && p.ordStatus == "new" && p.text == "end").Count() != 0)
                    {
                        string orderID = recent_orders.Where(p => p.side == side && p.price == price && p.ordStatus == "new" && p.text == "end").FirstOrDefault().orderID;
                        int orderQty = recent_orders.Where(p => p.side == side && p.price == price && p.ordStatus == "new" && p.text == "end").FirstOrDefault().orderQty + item.cumQty;
                        bitemex.PostOrders_PUT(orderID, price, orderQty);
                    }
                    else
                    {
                        _bitmex_order = new bitmex_order();
                        _bitmex_order.symbol = "XBTUSD";
                        _bitmex_order.side = side;
                        _bitmex_order.orderQty = item.cumQty;
                        _bitmex_order.price = price;
                        _bitmex_order.ordType = "Limit";
                        _bitmex_order.text = "end";
                        list_bitmex_order.Add(_bitmex_order);
                    }


                }
                bitemex.PostOrders_bulk(list_bitmex_order);
            }


            // [ end ]
            pre_timestamp = bitemex_Bucketeds[0].timestamp;
            no++;
        }

        private bitmex_order get_bitmex_order(string side, bitemex_position bitemex_position, double standard_avgCostPrice, 
            double close, double iniinitial_value, double step_spring, int step_qty)
        {
            if (side == "Buy")
            {
                double price = iniinitial_value - step_spring;
                price = close <= price ? close - 1.5 : price;
                if (bitemex_position.currentQty == 0 || (bitemex_position.currentQty > 0 && standard_avgCostPrice > price) || (bitemex_position.currentQty < 0 && bitemex_position.marginCallPrice > price))
                {
                    bitmex_order _bitmex_order = new bitmex_order();
                    _bitmex_order.symbol = "XBTUSD";
                    _bitmex_order.side = "Buy";
                    _bitmex_order.clOrdID = no.ToString();
                    _bitmex_order.orderQty = step_qty;
                    _bitmex_order.price = price;
                    _bitmex_order.ordType = "Limit";
                    return _bitmex_order;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                double price = iniinitial_value + step_spring;
                price = close >= price ? close + 1.5 : price;
                if (bitemex_position.currentQty == 0 || (bitemex_position.currentQty < 0 && standard_avgCostPrice < price) || (bitemex_position.currentQty > 0 && bitemex_position.marginCallPrice < price))
                {
                    bitmex_order _bitmex_order = new bitmex_order();
                    _bitmex_order.symbol = "XBTUSD";
                    _bitmex_order.side = "Sell";
                    _bitmex_order.clOrdID = no.ToString();
                    _bitmex_order.orderQty = step_qty;
                    _bitmex_order.price = price;
                    _bitmex_order.ordType = "Limit";
                    return _bitmex_order;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
