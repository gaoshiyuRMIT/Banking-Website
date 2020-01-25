using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using Banking.ViewModels;

namespace Banking.Models
{
    public class SessionKeyBase
    {
        public static T GetFromSession<T>(ISession session, string key)
        {
            string json = session.GetString(key);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void SetToSession<T>(ISession session, string key, T model)
        {
            string json = JsonConvert.SerializeObject(model);
            session.SetString(key, json);
        }
    }

    public class BillPaySessionKey : SessionKeyBase
    {
        public static string EditOrCreate = "BillPayEditOrCreateViewModel";

        public static BillPayEditViewModel GetEditViewModelFromSession(ISession session)
        {
            return GetFromSession<BillPayEditViewModel>(session, EditOrCreate);
        }

        public static void SetEditViewModelToSession(BillPayEditViewModel model, ISession session)
        {
            SetToSession(session, EditOrCreate, model);
        }
    }
}
