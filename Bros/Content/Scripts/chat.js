$(function () {
	emotify.emoticons("/Content/Images/Smileys/", false, {
		"-_-": ["-_-.png", "annoyed"],
		"=B": ["=B.png", "buckteeth", ":B", ":-B", "=-B"],
		"D=<": ["angry.png", "angry", "D:<"],
		":bomb:": ["bomb.png", "bomb"],
		":boo:": ["boo.png", "ghost"],
		":cool:": ["cool.png", "cool"],
		"='(": ["cry.png", "cry", ":'("],
		"@@_@@": ["dizzy.png", "dizzy"],
		":drunk:": ["drunk.png", "drunk"],
		":evil:": ["evil.png", "evil"],
		"=D": ["grin.png", "grin", ":D", ":-D", "=-D"],
		"=)": ["happy.png", "happy", ":)", ":-)", "=-)"],
		"^_^": ["cute.png", "cute"],
		":inlove:": ["inlove.png", "inlove"],
		"=*": ["kiss.png", "kiss", ":*", ":-*", "=-*"],
		":meow:": ["meow.png", "meow"],
		":ninja:": ["ninja.png", "ninja"],
		"O_O": ["O_O.png", "O_O", "0_0", "o_o"],
		":omg:": ["omg.png", "omg"],
		":onfire:": ["onfire.png", "onfire"],
		":ouch:": ["ouch.png", "ouch"],
		":serious:": ["serious.png", "serious"],
		":sick:": ["sick.png", "sick"],
		":sleep:": ["sleep.png", "sleep"],
		"=<": ["snooty.png", "snooty", ":<", ":-<", "=-<"],
		"o_O": ["wut.png", "wut", "o_0"],
		"x_x": ["x_x.png", "dead", "X_X"],
		"xD": ["XD.png", "XD", "XD"],
		":yarr:": ["yarr.png", "yarr"],
		":zombie:": ["zombie.png", "zombie"]
	});

	var selfId = $("#me").attr("data-user-id");
	var selfName = $("#me").text();

	// Proxy created on the fly
	var chat = $.connection.chatHub;

	// Declare a function on the chat hub so the server can invoke it
	chat.client.receive = function (senderId, receiverId, message) {
		message = emotify(message);
		var fromSelf = senderId == selfId;
		var userId = fromSelf ? receiverId : senderId;
		$form = createChatWindow(userId);

		if (!$form.hasClass("in-focus")) {
			$form.addClass("flash");
		} else {
			chat.server.readMessage($form.attr("data-user-id"));
		}

		var userName = fromSelf ? "me" : $(".receiverName", $form).text();
		var uClass = fromSelf ? "me" : "them";
		$(".messages", $form).append("<p><strong class=\"" + uClass + "\">" + userName + ":</strong> " + message + "</p>");
		$('.msgWrap', $form).stop().animate({ scrollTop: $('.messages', $form).height() })
	};

	chat.client.friendList = function (friends) {
		for (var i = 0; i < friends.length; i++) {
			chat.client.friendOnline(friends[i]);
		}
	};

	chat.client.friendOffline = function (friendId) {
		$("#friend-list li[data-user-id=" + friendId + "]").removeClass("online").addClass("offline");
	};

	chat.client.friendOnline = function (friendId) {
		$("#friend-list li[data-user-id=" + friendId + "]").removeClass("offline").addClass("online");
	};

	// Declare UI actions
	var createChatWindow = function (userId, focus) {
		var $form = $(".chat-window[data-user-id=" + userId + "]");
		if ($form.length == 0) {
			$form = $("#templateForm").clone();
			$form.attr("data-user-id", userId);
			$(".receiverName", $form).text($("#friend-list li[data-user-id=" + userId + "]").text());
			$("body").append($form);
			$form.css("top", (150 * ($(".chat-window").length - 1)) + "px");
			$form.css("left", (60 * ($(".chat-window").length - 1)) + "px");
			ui_bindings();
		}

		$form.fadeIn(200);

		if (focus === true) {
			$(".chat-window").removeClass("in-focus");
			$form.addClass("in-focus");
			$(".messageBox", $form).focus();
		}

		return $form;
	};

	var ui_bindings = function () {
		$(".chat-window").draggable({ handle: ".header", opacity: .9, distance: 0 });
	};

	$(".chat-window").live("submit", function () {
		var receiver = $(this).attr("data-user-id");
		chat.server.send(receiver, $('.messageBox', this).val());
		$('.messageBox', this).val("");
		return false;
	});

	$(".chat-window .close").live("click", function () {
		$(this).parent().fadeOut(200);
	});

	$(".chat-window").live("click", function () {
		$(".chat-window").removeClass("in-focus");
		$(this).addClass("in-focus");

		if ($(this).hasClass("flash")) {
			chat.server.readMessage($(this).attr("data-user-id"));
			$(this).removeClass("flash");
		}

	});

	$("#friend-list li").click(function () {
		createChatWindow($(this).attr("data-user-id"), true);
	});


	// Start the connection
	$.connection.hub.start(function () {
		chat.server.authenticate();
	});

});