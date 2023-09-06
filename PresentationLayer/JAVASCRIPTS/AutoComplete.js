function Autocomplete() {

    //City
    $(function() {
        $(".tbCity").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: "../WebService.asmx/GetData",
                    data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_CITY','col1': 'CITYNO','col2': 'CITY','col3': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            },
            minLength: 1
        });
    });

    //State
    $(function() {
        $(".tbState").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: "../WebService.asmx/GetData",
                    data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_STATE','col1': 'STATENO','col2': 'STATENAME','col3': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            },
            minLength: 1
        });
    });

    //Religion
    $(function() {
        $(".tbReligion").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: "../WebService.asmx/GetData",
                    data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_RELIGION','col1': 'RELIGIONNO','col2': 'RELIGION','col3': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            },
            minLength: 1
        });
    });

    //Nationality
    $(function() {
        $(".tbNationality").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: "../WebService.asmx/GetData",
                    data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_NATIONALITY','col1': 'NATIONALITYNO','col2': 'NATIONALITY','col3': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            },
            minLength: 1
        });
    });

    //Category
    $(function() {
        $(".tbCategory").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: "../WebService.asmx/GetData",
                    data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_CATEGORY','col1': 'CATEGORYNO','col2': 'CATEGORY','col3': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            },
            minLength: 1
        });
    });


    //State
    $(function() {
        $(".tbState1").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: "../WebService.asmx/GetData",
                    data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_STATE','col1': 'STATENO','col2': 'STATENAME','col3': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            },
            minLength: 1
        });
    });
    
     //blood group
    $(function() {
        $(".tbBloodGrp").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: "../WebService.asmx/GetData",
                    data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_BLOODGRP','col1': 'BLOODGRPNO','col2': 'BLOODGRPNAME','col3': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            },
            minLength: 1
        });
    });
}