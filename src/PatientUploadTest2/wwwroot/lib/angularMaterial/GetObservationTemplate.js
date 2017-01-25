(function () {
    //'use strict';
    angular
        .module('GetObservationApp', ['ngMaterial'])
        .controller('GetObservation', DemoCtrl);

    function DemoCtrl($timeout, $q, $log, $http, $scope) {
        var self = this;

        self.simulateQuery = false;
        self.isDisabled = false;
       
        
        // list of `state` value/display objects
       
        
        
        self.states = loadAllProducts($http);
        
        self.querySearch = querySearch;
        self.selectedItemChange = selectedItemChange;
        self.searchTextChange = searchTextChange;

        self.newState = newState;

        function newState(state) {
            alert("Sorry! You'll need to create a Constitution for " + state + " first!");
        }

        // ******************************
        // Internal methods
        // ******************************


        function loadAllProducts($http)  
        {  
            var allProducts = [];  
            var url = '';  
            var result = [];  
            url = '/Countries/AutocompleteForJS';  
            $http({  
                    method: 'GET',  
                    url: url,  
            }).then(function successCallback(response) {
                $log.info("Data received from the server: " + response.data);
                allProducts = response.data.split(',');  
                angular.forEach(allProducts, function (product, key) {

                   result.push({  
                                  value: product.toLowerCase(),  
                                  display: product  
                             });  
                });  
                }, function errorCallback(response){  
                           console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);  
                });

             return result;  
          }  


        /**
         * Search for states... use $timeout to simulate
         * remote dataservice call.
         */
        function querySearch(query) {
            $log.info("Data Received from server (Outside the function): " + self.states);
            var results = query ? self.states.filter(createFilterFor(query)) : self.states,
                deferred;
            if (self.simulateQuery) {
                deferred = $q.defer();
                $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
                return deferred.promise;
            } else {
                return results;
            }
        }

        function searchTextChange(text) {
            $log.info('Text changed to ' + text);
        }

        function selectedItemChange(item) {
            $log.info('Item changed to ' + JSON.stringify(item));
        }

        /**
         * Build `states` list of key/value pairs
         */
        function loadAll() {
            /*
            var allStates = 'Alabama, Alaska, Arizona, Arkansas, California, Colorado, Connecticut, Delaware,\
              Florida, Georgia, Hawaii, Idaho, Illinois, Indiana, Iowa, Kansas, Kentucky, Louisiana,\
              Maine, Maryland, Massachusetts, Michigan, Minnesota, Mississippi, Missouri, Montana,\
              Nebraska, Nevada, New Hampshire, New Jersey, New Mexico, New York, North Carolina,\
              North Dakota, Ohio, Oklahoma, Oregon, Pennsylvania, Rhode Island, South Carolina,\
              South Dakota, Tennessee, Texas, Utah, Vermont, Virginia, Washington, West Virginia,\
              Wisconsin, Wyoming';
            */
            
            //$scope.allStates = "";
            
            var resultFromServer = $http.get('/Reports/GetObservationTemplate').then(function (response) {
                //console.log(response);
                console.log("Data Received from server (Inside the function): " + response.data);
                $scope.temp = response.data;
                
            })
            console.log("Data Received from server (outside the function): " + self.states);
            allstates = self.states;
            console.log("Splitted String (outside the function): " + self.states.split(','));
            // return allStates.split(/, +/g).map(function (state) {
            return allStates.split(",").map(function (state) {
                return {
                    
                    value: state.toLowerCase(),
                    display: state
                };
            });
        }

        /**
         * Create filter function for a query string
         */
        function createFilterFor(query) {
            var lowercaseQuery = angular.lowercase(query);

            return function filterFn(state) {
                return (state.value.indexOf(lowercaseQuery) === 0);
            };

        }
    }
})();