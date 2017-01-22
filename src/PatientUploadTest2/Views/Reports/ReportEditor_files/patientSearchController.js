var patientSearch = angular.module('PatientSearch',[]);

patientSearch.controller('patientSearchController', function patientSearchController($scope, $http){
	$scope.patients = [
{
	"id": "87654321"
	"inpatient_id" : "123456789"
	"archive_id" : "00123"
	"name" : "刘会计"
	"sex": "男"
	"age": "32"
	"paytype": "医疗保险"
}
{
	"id": "87654321"
	"inpatient_id" : "123456789"
	"archive_id" : "00123"
	"name" : "刘会计"
	"sex": "男"
	"age": "32"
	"paytype": "医疗保险"
}
{
	"id": "87654321"
	"inpatient_id" : "123456789"
	"archive_id" : "00123"
	"name" : "刘会计"
	"sex": "男"
	"age": "32"
	"paytype": "医疗保险"
}
];
	$scope.searchSubmit = function (){
		$http({
		method: 'JSONP'
		url:'localhost:9876/debug.html'
		data: $scope.patientID
	}).then(function successCallback(response){
		alert("Server get data")
	}, function errorCallback(response){
		alert("Something wrong with the Server")
	})
	}

	


	//$http.get("/testdata.txt").then(function (response){
			
	//		if (response) {
	//			alert("Data From Server Received!")
	//			$scope.patients = response
	//		};
	//	})
	
})