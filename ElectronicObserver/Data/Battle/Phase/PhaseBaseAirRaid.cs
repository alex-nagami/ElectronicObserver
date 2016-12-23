﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicObserver.Data.Battle.Phase {

	/// <summary>
	/// 基地空襲戦フェーズ
	/// </summary>
	public class PhaseBaseAirRaid : PhaseAirBattleBase {

		private BattleBaseAirCorpsSquadron[] _squadrons;
		/// <summary>
		/// 参加した航空中隊データ
		/// </summary>
		public ReadOnlyCollection<BattleBaseAirCorpsSquadron> Squadrons {
			get { return Array.AsReadOnly( _squadrons ); }
		}

		private IEnumerable<BattleBaseAirCorpsSquadron> GetSquadrons() {
			foreach ( dynamic d in AirBattleData.api_squadron_plane ) {
				if ( !( d is Codeplex.Data.DynamicJson ) )
					continue;
				if ( !d.IsArray )
					continue;

				foreach ( dynamic e in d )
					yield return new BattleBaseAirCorpsSquadron( e );
			}
		}


		public PhaseBaseAirRaid( BattleData data, string title )
			: base( data, title ) {

			AirBattleData = data.RawData.api_air_base_attack;
			StageFlag = AirBattleData.api_stage_flag() ? (int[])AirBattleData.api_stage_flag : null;

			LaunchedShipIndexFriend = GetLaunchedShipIndex( 0 );
			LaunchedShipIndexEnemy = GetLaunchedShipIndex( 1 );

			_squadrons = GetSquadrons().ToArray();

			TorpedoFlags = ConcatStage3Array( "api_frai_flag", "api_erai_flag" );
			BomberFlags = ConcatStage3Array( "api_fbak_flag", "api_ebak_flag" );
			Criticals = ConcatStage3Array( "api_fcl_flag", "api_ecl_flag" );
			Damages = ConcatStage3Array( "api_fdam", "api_edam" );
		}

		public override void EmulateBattle( int[] hps, int[] damages ) {

			if ( !IsAvailable ) return;

			CalculateAttack( -1, hps );
		}

	}
}
