﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BiliRanking.Core
{
    public class BiliParse
    {
        /// <summary>
        /// 排序类型。注意：Default和New需要ToLower！
        /// </summary>
        public enum SortType
        {
            [Description("播放数")]
            hot,
            [Description("按新投稿排序")]
            Default,
            [Description("按新评论排序")]
            New,
            [Description("评论数")]
            review,
            [Description("弹幕数")]
            damku,
            [Description("用户评分")]
            comment,
            [Description("硬币数")]
            promote,
            [Description("按标题拼音排序")]
            pinyin,
            [Description("收藏（不存在于API文档）")]
            stow
        }

        public static List<string> GetList(SortType type, int zone, int page, DateTime from, DateTime to)
        {
            Log.Info("正在获取排行 - 依据" + type.ToString().ToLower() + "/分区" + zone + "/分页" + page + "/时间" + from.ToString("yyyy-MM-dd") + "~" + to.ToString("yyyy-MM-dd"));
            string url = "http://www.bilibili.com/list/" + type.ToString() + "-" + zone + "-" + page + "-" + from.ToString("yyyy-MM-dd") + "~" + to.ToString("yyyy-MM-dd") + ".html";
            string html = BiliInterface.GetHtml(url);
            if (html == null) return null;
            int p = html.IndexOf("href=\"/video/av");
            List<string> r = new List<string>();
            while (p > 0)
            {
                string s = html.Substring(p + 13, html.IndexOf("/", p + 13) - p - 13);
                if (!r.Contains(s))
                    r.Add(s);
                p = html.IndexOf("href=\"/video/av", p + 3);
            }
            return r;
        }
    }
}
