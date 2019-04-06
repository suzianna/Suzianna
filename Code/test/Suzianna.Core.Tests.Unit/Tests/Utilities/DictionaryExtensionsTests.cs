using System.Collections.Generic;
using NFluent;
using Suzianna.Core.Utilities;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Utilities
{
    public class DictionaryExtensionsTests
    {
        private Dictionary<string, string> _dicitonary;
        public string key;
        public string firstValue;
        public string secondValue;
        public DictionaryExtensionsTests()
        {
            this._dicitonary = new Dictionary<string, string>();
            this.key = "SampleKey";
            this.firstValue = "SomeValue1";
            this.secondValue = "SomeValue2";
        }

        [Fact]
        public void should_add_item_to_dictionary_when_its_not_exist()
        {
            this._dicitonary.AddOrUpdate(key, firstValue);

            Check.That(_dicitonary).ContainsPair(key, firstValue);
        }

        [Fact]
        public void should_add_pair_to_dictionary_when_its_not_exist()
        {
            this._dicitonary.AddOrUpdate(new KeyValuePair<string, string>(key, firstValue));

            Check.That(_dicitonary).ContainsPair(key, firstValue);
        }

        [Fact]
        public void should_update_item_in_dictionary_when_it_exists()
        {
            this._dicitonary.AddOrUpdate(key, firstValue);
            this._dicitonary.AddOrUpdate(key, secondValue);

            Check.That(_dicitonary).ContainsPair(key, secondValue);
        }

        [Fact]
        public void should_update_pair_in_dictionary_when_it_exists()
        {
            this._dicitonary.AddOrUpdate(new KeyValuePair<string, string>(key, firstValue));
            this._dicitonary.AddOrUpdate(new KeyValuePair<string, string>(key, secondValue));

            Check.That(_dicitonary).ContainsPair(key, secondValue);
        }

        [Fact]
        public void should_return_dictionary_itself_after_adding_item()
        {
            var output = this._dicitonary.AddOrUpdate(key, firstValue);

            Check.That(output).IsSameReferenceAs(this._dicitonary);
        }
        [Fact]
        public void should_return_dictionary_itself_after_adding_pair()
        {
            var output = this._dicitonary.AddOrUpdate(new KeyValuePair<string, string>(key, firstValue));

            Check.That(output).IsSameReferenceAs(this._dicitonary);
        }
    }
}
