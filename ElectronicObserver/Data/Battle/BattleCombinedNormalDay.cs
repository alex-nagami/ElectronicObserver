﻿using ElectronicObserver.Data.Battle.Phase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicObserver.Data.Battle {

	/// <summary>
	/// 連合艦隊(機動部隊)昼戦
	/// </summary>
	public class BattleCombinedNormalDay : BattleDay {

		public override void LoadFromResponse( string apiname, dynamic data ) {
			base.LoadFromResponse( apiname, (object)data );

			BaseAirAttack = new PhaseBaseAirAttack( this );
			AirBattle = new PhaseAirBattle( this );
			Support = new PhaseSupport( this );
			OpeningASW = new PhaseOpeningASW( this, true );
			OpeningTorpedo = new PhaseTorpedo( this, 0 );
			Shelling1 = new PhaseShelling( this, 1, "1", true );
			Torpedo = new PhaseTorpedo( this, 2 );
			Shelling2 = new PhaseShelling( this, 3, "2", false );
			Shelling3 = new PhaseShelling( this, 4, "3", false );


			BaseAirAttack.EmulateBattle( _resultHPs, _attackDamages );
			AirBattle.EmulateBattle( _resultHPs, _attackDamages );
			Support.EmulateBattle( _resultHPs, _attackDamages );
			OpeningASW.EmulateBattle( _resultHPs, _attackDamages );
			OpeningTorpedo.EmulateBattle( _resultHPs, _attackDamages );
			Shelling1.EmulateBattle( _resultHPs, _attackDamages );
			Torpedo.EmulateBattle( _resultHPs, _attackDamages );
			Shelling2.EmulateBattle( _resultHPs, _attackDamages );
			Shelling3.EmulateBattle( _resultHPs, _attackDamages );

		}


		public override string APIName {
			get { return "api_req_combined_battle/battle"; }
		}

		public override BattleData.BattleTypeFlag BattleType {
			get { return BattleTypeFlag.Day | BattleTypeFlag.Combined; }
		}

		public override string GetBattleDetail( int index ) {
			var sb = new StringBuilder();

			string baseair = BaseAirAttack.GetBattleDetail( index );
			string airbattle = AirBattle.GetBattleDetail( index );
			string support = Support.GetBattleDetail( index );
			string asw = OpeningASW.GetBattleDetail( index );
			string openingTorpedo = OpeningTorpedo.GetBattleDetail( index );
			string shelling1 = Shelling1.GetBattleDetail( index );
			string torpedo = Torpedo.GetBattleDetail( index );
			string shelling2 = Shelling2.GetBattleDetail( index );
			string shelling3 = Shelling3.GetBattleDetail( index );

			if ( baseair != null )
				sb.AppendLine( "《基地航空队攻击》" ).Append( baseair );
			if ( airbattle != null )
				sb.AppendLine( "《航空战》" ).Append( airbattle );
			if ( support != null )
				sb.AppendLine( "《支援攻击》" ).Append( support );
			if ( asw != null )
				sb.AppendLine( "《开幕对潜》" ).Append( asw );
			if ( openingTorpedo != null )
				sb.AppendLine( "《开幕雷击》" ).Append( openingTorpedo );
			if ( shelling1 != null )
				sb.AppendLine( "《第一次炮击战》" ).Append( shelling1 );
			if ( torpedo != null )
				sb.AppendLine( "《雷击战》" ).Append( torpedo );
			if ( shelling2 != null )
				sb.AppendLine( "《第二次炮击战》" ).Append( shelling2 );
			if ( shelling3 != null )
				sb.AppendLine( "《第三次炮击战》" ).Append( shelling3 );

			return sb.ToString();
		}
	}

}
