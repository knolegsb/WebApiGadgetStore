angular.module('gadgetsStore')
	.constant('gadgetsUrl', 'http://localhost:11020/api/gadgets')
	.constant('ordersUrl', 'http://localhost:11020/api/orders')
	.constant('categoriesUrl', 'http://localhost:11020/api/categories')
    .constant('tempOrdersUrl', 'http://localhost:11020/api/sessions/temporders')
	.controller('gadgetStoreCtrl', function ($scope, $http, $location, gadgetsUrl, categoriesUrl, ordersUrl, tempOrdersUrl, cart) {

	    $scope.data = {};

	    $http.get(gadgetsUrl)
			.then(function (result) {
			    $scope.data.gadgets = result.data;
			})
			.catch(function (error) {
			    $scope.data.error = error;
			});

	    $http.get(categoriesUrl)
        .then(function (result) {
            $scope.data.categories = result.data;
        })
        .catch(function (error) {
            $scope.data.error = error;
        });

	    $scope.sendOrder = function (shippingDetails) {
	        var order = angular.copy(shippingDetails);
	        order.gadgets = cart.getProducts();
	        $http.post(ordersUrl, order)
			.then(function (data, status, headers, config) {
			    $scope.data.OrderLocation = headers('Location');
			    $scope.data.OrderID = data.data.OrderID;
			    cart.getProducts().length = 0;
			    $scope.saveOrder();
			})
			.catch(function (error) {
			    $scope.data.orderError = error;
			}).finally(function () {
			    $location.path("/complete");
			});
	    }

	    $scope.saveOrder = function () {
	        var currentProducts = cart.getProducts();

	        $http.post(tempOrdersUrl, currentProducts)
			    .then(function (data, status, headers, config) {
			    }).catch(function (error) {
			    }).finally(function () {
			    });
	    }

	    $scope.checkSessionGadgets = function () {
	        $http.get(tempOrdersUrl)
            .then(function (data) {
                if (data) {
                    for (var i = 0; i < data.length; i++) {
                        var item = data.data[i];
                        cart.pushItem(item);
                    }
                }
            })
            .catch(function (error) {
                console.log('error checking session: ' + error);
            });
	    }

	    $scope.showFilter = function () {
	        return $location.path() == '';
	    }

	    $scope.checkoutComplete = function () {
	        return $location.path() == '/complete';
	    }
	});