var httpVerbs = {
    POST: 'POST',
    PUT: 'PUT',
    GET: 'GET',
    DEL: 'DELETE'
};
var homesDataService = (function () {
    var
       ds = {
           commit: function (type, url, data) {

               // Remove 'id' member to perpare for INSERT
               if (type === httpVerbs.POST) {
                   delete data['id'];
               }

               return $.ajax({
                   type: type,
                   url: url,
                   data: data,
                   dataType: 'json'
               });
           },

           del: function (data) {
               return this.commit(httpVerbs.DEL, '/api/homes/' + data.id);
           },

           save: function (data) {

               var
                   type = httpVerbs.POST,
                   url = '/api/homes';

               if (data.id > 0) {
                   type = httpVerbs.PUT;
                   url += '/' + data.id;
               }

               return this.commit(type, url, data);
           },

           saveImage: function (data) {
               return $.ajax({
                   type: httpVerbs.POST,
                   url: '/homes/uploadimage',
                   processData: false,
                   contentType: false,
                   data: data
               });
           },
           localKey: 'codedhomes-new-homes',

           saveLocal: function (newHome) {
               if (Modernizr.localstorage) {
                   var
                       ls = window.localStorage,
                       key = ds.localKey;

                   if (ls.getItem(key) == null) {
                       ls.setItem(key, '[]');
                   }

                   var existing = ls.getItem(key);
                   existing = ds.parse(existing);

                   existing.push(newHome)

                   var serialized = JSON.stringify(existing);
                   ls.setItem(key, serialized);
               }
           },

           // reviver syntax: http://www.json.org/js.html
           parse: function (data) {
               var parseWithReviver = function (d) {
                   return JSON.parse(d, function (key, value) {
                       var type;
                       if (value && typeof value === 'object') {
                           type = value.type;
                           if (typeof type === 'string' &&
                               typeof window[type] === 'function') {
                               return new (window[type])(value);
                           }
                       }
                       return value;
                   });
               };

               var result = parseWithReviver(data);

               if (_.isArray(result)) {
                   _.each(result, function (v, i) {
                       if (typeof v === 'string') {
                           result[i] = parseWithReviver(v);
                       }
                   });
               }

               return result;
           },

           delLocal: function (home) {
               var
                   key = homesDataService.localKey,
                   data = window.localStorage.getItem(key),
                   homes = {},
                   remaining = [];

               debugger;

               homes = ds.parse(data);

               // difference from array
               _.each(homes, function (v, i) {
                   if (!_.isEqual(homes[i], home)) {
                       remaining.push(homes[i]);
                   }
               });

               if (remaining.length > 0) {
                   remaining = JSON.stringify(remaining);
               } else {
                   remaining = '[]';
               }

               window.localStorage.setItem(key, remaining);
           },
       };

    _.bindAll(ds, 'del', 'save');

    return {
        save: ds.save,
        saveLocal: ds.saveLocal,
        saveImage: ds.saveImage,
        del: ds.del,
        delLocal: ds.delLocal,
        parse: ds.parse,
        localKey: ds.localKey
    }
})();