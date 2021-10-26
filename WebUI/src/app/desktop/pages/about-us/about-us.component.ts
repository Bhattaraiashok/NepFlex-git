import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.scss']
})
export class AboutUsComponent implements OnInit {
  aboutUsText: string;
  aboutUsTitle: string;
  constructor() {
    this.infoPage();
  }

  ngOnInit() {}
  infoPage() {
    this.aboutUsTitle = `About Us`;
    this.aboutUsText = `
    <div class="aboutus">
      <p class="head_para">
        <strong>General Info about Application: </strong>
      </p>
      <p>NepaliCraig is designed and developed for all the people to search or post their needs.</p>
      <p>if you have question on how it works?:</p>
      <p>Example: I have a small brother who studies in class 4 and he is upgrading by 1 this year. Here he/we might want to
        list his old books and notes publicly so that somebody willing can reach out.</p>
      <p>Old stuff might fully occupy our home but cannot afford much to buy new one. If they publicized the item someone might
        reach out to them and they can get some amount of their old stuffs mentioning how the condition is and amount to
        that.</p>
      <p class="head_para">
        <strong> who are actively searching.</strong>
      </p>
      <p>Let&rsquo;s suppose Mr. ABCD wants a new or old item. He posts into NC and gets viewed by public. Public tries to reach
        out to him saying &ldquo;Hey, I have this option. If you like we can meet and talk&rdquo; he meets him gets the item
        he wanted on his expected cost. He happily takes that item to home because NC made so easier to connect people&rsquo;s
        choice.
      </p>
      <p class="head_para">
        <strong>You have a Business? </strong>
      </p>
      <p>NepaliCraig will be so useful to enhance your business and grow your customer.</p>
      <p>You can advertise with us. ( NepaliCraig is focused to advertise only selected ads not all)</p>
      <p>You can briefly create a post about your business and your offers.</p>
      <p>Business representative can post their free discounted coupons on our coupon page which will help them to gain maximum
        number of customer through their offers.</p>
      <p>&nbsp;</p>
      <p class="head_para">
        <strong>To public who want to post their article/poem/news/views?</strong>
      </p>
      <p>It is now much easier to you to post your article and make it view by large number of people. NC provides it for Free.</p>
      <p>Though children under 15 should be monitored by their parents to use this website but may post their article and poems
        on ArticleList page</p>
      <p>NC articles poster should read our terms and condition before writing.</p>

      <p class="head_para">To all the event posters.</p>
      <p>Are you having trouble to make your event announce on radio or television set? No worry, you have great time now to
        post your event on Nepalicraig-event subsection 100% free.</p>
      <p>&nbsp;</p>
      <p class="head_para">Fraud Items?</p>
      <p>Well, items posted on this application might be genuine or fraud. If you find any fraud item please report us at report
        item section.</p>
      <p>NepaliCraig brings this application among people so that it makes their search easy. Hence, such fraud reported item
        will be deleted and will be notified to the user not to post any fraud item else if repeatedly than according to
        terms and condition of NepaliCraig, it can take further step to disable&nbsp; that userid ,IPAddress, EmailID</p>
      <p>If userIP is blocked you have to personally mail us to unblock your IPAddress and should make us understand you are
        not going to post such items again.</p>
      <p>NepaliCraig also want a favor from you to make this application reach out to all. If you find any bugs (issues) with
        in application please give us feedback with the URL you got issue with.</p>

      <p>&nbsp;</p>
      <p>
        <a href="terms-of-use"> See Terms and Condition</a>
      </p>
      <p>&nbsp;</p>
      <p>GOOD LUCK.</p>
      <p>&nbsp;</p>
      <p>
        <strong>NepaliCraig Tm</strong>
      </p>

    </div>
    `;
  }
}
