using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bit.Model;
using BitMEX;
using Newtonsoft.Json;




namespace bit
{
    public partial class Form1 : Form
    {

        // jejuairfarm
        private static string bitmexKey = "1jLqib5tAmDF29DBb8M9292R";
        private static string bitmexSecret = "aI2c215XvsqSa-XkAyP83CmJnPM07Cuw-ysbRYG7pn8BFEHi";

        // [2]
        BitMEXApi bitemex = new BitMEXApi(bitmexKey, bitmexSecret);
        bitemex_position bitemex_position = new bitemex_position();
        List<bitmex_order> bitmex_orders = new List<bitmex_order>();
        List<bitmex_bucketed> btmex_Bucketeds = new List<bitmex_bucketed>();
        // [2-1] 
        BitMex_ActionClass.BitMex_ActionClass bitmex_ActionClass = new BitMex_ActionClass.BitMex_ActionClass();

        // [3]
        Timer timer1 = new Timer();

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        private void Timer1_Tick(object Sender, EventArgs e)
        {
            Auto_Trad_Play();
        }

        int second = -1;
        int timeloop = 5;  //고정값
        Boolean readPosition = false;
        string pre_timestamp = "";
        user_margin _user_margin = new user_margin();
        private void Auto_Trad_Play()
        {
            //[1]
            if (second < 0 || second > 48)  //53초 이후부터는 timeloop초에 한번 주문 
            {
                //[2-1] 48초 이후 1회 실행
                if (readPosition == false)
                {
                    //[1] Positions
                    GetPositions();
                    readPosition = true;
                }

                //[2-2] 56초 이후 주문 들어갈때까지 계속 실행
                bitmex_Get_bucketed_2();
                if (pre_timestamp != btmex_Bucketeds[0].timestamp)
                {
                    bitmex_ActionClass.order_System2(btmex_Bucketeds, bitemex_position);
                    second = 0;
                    readPosition = false;
                    pre_timestamp = btmex_Bucketeds[0].timestamp;
                }
            }
            //[end]
            second = second + timeloop;

        }

        private void btn_Start_Click_1(object sender, EventArgs e)
        {
            btn_Start.Enabled = false;
            btn_Stop.Enabled = true;

            Auto_Trad_Play();

            // timer1의 속성 정의 – 코드에서 Timer 생성
            timer1.Enabled = true;
            timer1.Interval = timeloop * 1000;
            timer1.Tick += Timer1_Tick;
        }

        private void bitmex_Get_bucketed_2()
        {
            try
            {
                /// [1] 봉 얻어오기
                var result_bucketed = bitemex.bitmex_Get_bucketed("1m", true, "XBTUSD", 3, true);
                List<bitmex_bucketed> bucketeds = new List<bitmex_bucketed>();
                btmex_Bucketeds = JsonConvert.DeserializeObject<List<bitmex_bucketed>>(result_bucketed);
            }
            catch (Exception ex)
            {
                txt_position.Text = "봉 불러오기 error";
                throw;
            }
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            btn_Start.Enabled = true;
            btn_Stop.Enabled = false;
            timer1.Enabled = false;
            txt_position.AppendText("\r\n타이머가 중지 되었습니다.");
        }


        private void GetPositions()
        {
            var json_result = bitemex.GetPositions("{ \"symbol\" : \"XBTUSD\" }");
            List<bitemex_position> _bitemex_positions = new List<bitemex_position>();
            _bitemex_positions = JsonConvert.DeserializeObject<List<bitemex_position>>(json_result);

            if (_bitemex_positions.Count == 1)
            {
                // 포지션은 무조건 1개 밖에 없을 것임....
                if (_bitemex_positions[0].currentQty == 0)
                {
                    bitemex_position.account = 0;
                    bitemex_position.symbol = "";
                    bitemex_position.currentQty = 0;
                    bitemex_position.avgCostPrice = 0.0;
                    bitemex_position.marginCallPrice = 0.0;
                    bitemex_position.liquidationPrice = 0.0;
                }
                else
                {
                    bitemex_position.account = _bitemex_positions[0].account;
                    bitemex_position.symbol = _bitemex_positions[0].symbol;
                    bitemex_position.currentQty = _bitemex_positions[0].currentQty;
                    bitemex_position.avgCostPrice = _bitemex_positions[0].avgCostPrice;
                    bitemex_position.marginCallPrice = _bitemex_positions[0].marginCallPrice;
                    bitemex_position.liquidationPrice = _bitemex_positions[0].liquidationPrice;
                }
            }
            else
            {
                txt_position.AppendText("포지션이 0개이거나 2개이상임. 확인해볼것~.\r\n");
                // 0개 혹은 2개도 있나 확인해보자.... 아마 없을것임...
            }
        }
        private void btn_balance_Click(object sender, EventArgs e)
        {
            _user_margin = JsonConvert.DeserializeObject<user_margin>(bitemex.GetUserMargin());
            txt_position.AppendText("○ " + DateTime.Now.ToString("MM월dd일 HH시mm분") +
                 "         " + string.Format("{0:#,###}", _user_margin.walletBalance * 0.1) +
                 "         " + string.Format("{0:#,###}", _user_margin.marginBalance * 0.1) + "\r\n");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
