using Integration.Common;
using Integration.Backend;
using System.Collections.Generic;

namespace Integration.Service
{
    public sealed class ItemIntegrationService
    {
        private readonly ItemOperationBackend itemIntegrationBackend = new();
        private static readonly Dictionary<string, object> processedItems = new Dictionary<string, object>();
        private static readonly object lockObject = new object();

        public Result SaveItem(string itemContent)
        {
            lock (lockObject)
            {
                if (processedItems.ContainsKey(itemContent))
                {
                    return new Result(false, $"Duplicate item received with content {itemContent}.");
                }
                else
                {
                    processedItems.Add(itemContent, itemContent);
                }
            }

            var item = itemIntegrationBackend.SaveItem(itemContent);
            return new Result(true, $"Item with content {itemContent} saved with id {item.Id}");
        }

        public List<Item> GetAllItems()
        {
            return itemIntegrationBackend.GetAllItems();
        }
    }
}
